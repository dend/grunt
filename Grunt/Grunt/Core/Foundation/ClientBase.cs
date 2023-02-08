// <copyright file="ClientBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenSpartan.Grunt.Converters;
using OpenSpartan.Grunt.Models;
using OpenSpartan.Grunt.Util;

namespace OpenSpartan.Grunt.Core.Foundation
{
    /// <summary>
    /// Class containing the fundamental pieces for an API client that talks to the Halo APIs.
    /// </summary>
    public abstract class ClientBase
    {
        private readonly JsonSerializerOptions serializerOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new EmptyDateStringToNullJsonConverter(),
                new XmlDurationToTimeSpanJsonConverter(),
            },
        };

        /// <summary>
        /// Gets or sets the Spartan token used to authenticate against the Halo Infinite API.
        /// </summary>
        public string SpartanToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the  player identifier in the format "xuid(XUID_VALUE)".
        /// </summary>
        public string Xuid { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the flight/clearance currently active for the player.
        /// </summary>
        public string ClearanceToken { get; set; } = string.Empty;

        /// <summary>
        /// Executes an API request in a standard way against a given API endpoint. This is a helper method that's put
        /// in place to simplify how the API calls are made because most requests against the Halo Infinite API are
        /// pretty repetitive.
        /// </summary>
        /// <param name="endpoint">The API endpoint to which the request is sent.</param>
        /// <param name="method">HTTP method to be used for the request.</param>
        /// <param name="useSpartanToken">Determines whether a Spartan token needs to be applied to teh request.</param>
        /// <param name="useClearance">Determines whether a clearance/flight ID needs to be applied to the request.</param>
        /// <param name="userAgent">User agent to be used for the request.</param>
        /// <param name="content">If the request contains data to be sent to the Halo Waypoint service, include it here. Expected format is JSON.</param>
        /// <param name="contentType">Content type for POST requests. By default it's `application/json`.</param>
        /// <typeparam name="T">Data type to return with the response metadata.</typeparam>
        /// <returns>Response string in case of a successful request. Null if request failed.</returns>
        public async Task<HaloApiResultContainer<T, HaloApiErrorContainer>> ExecuteAPIRequest<T>(string endpoint, HttpMethod method, bool useSpartanToken, bool useClearance, string userAgent, string content = "", ApiContentType contentType = ApiContentType.Json)
        {
            var contentTypeAttribute = contentType.GetHeaderValue();

            var client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,
            });

            HaloApiResultContainer<T, HaloApiErrorContainer> resultContainer = new(default!, new HaloApiErrorContainer());

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(endpoint),
                Method = method,
            };
            
            if (!string.IsNullOrEmpty(content))
            {
                request.Content = new StringContent(content, Encoding.UTF8, contentTypeAttribute);
            }

            if (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put || request.Method == HttpMethod.Patch)
            {
                request.Content ??= new StringContent(string.Empty);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentTypeAttribute is not null ? contentTypeAttribute : "application/json");
            }

            if (useSpartanToken)
            {
                request.Headers.Add("x-343-authorization-spartan", this.SpartanToken);
            }

            if (useClearance)
            {
                request.Headers.Add("343-clearance", this.ClearanceToken);
            }

            request.Headers.Add("User-Agent", userAgent);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");

            var response = await client.SendAsync(request);

            resultContainer.Error!.Code = Convert.ToInt32(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                if (typeof(T) == typeof(string))
                {
                    resultContainer.Result = (T)Convert.ChangeType(await response.Content.ReadAsStringAsync(), typeof(T));
                }
                else if (typeof(T) == typeof(byte[]))
                {
                    using MemoryStream dataStream = new();
                    response.Content.ReadAsStreamAsync().Result.CopyTo(dataStream);
                    resultContainer.Result = (T)Convert.ChangeType(dataStream.ToArray(), typeof(T));
                }
                else if (typeof(T) == typeof(bool))
                {
                    resultContainer.Result = (T)(object)response.IsSuccessStatusCode;
                }
                else
                {
                    // We will check whether the type is either one of the supported types or is
                    // a generic type, which means we're directly casting data to something that is usable
                    // without much custom model wrapping.
                    if (Attribute.GetCustomAttribute(typeof(T), typeof(IsAutomaticallySerializableAttribute)) != null ||
                        typeof(T).IsGenericType)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrWhiteSpace(responseString))
                        {
                            resultContainer.Result = JsonSerializer.Deserialize<T>(responseString, this.serializerOptions);
                        }
                    }
                    else
                    {
                        throw new NotSupportedException("The specified type is not supported. You can only get results in string or byte array formats.");
                    }
                }
            }

            if (response.Content != null)
            {
                resultContainer.Error.Message = await response.Content.ReadAsStringAsync();
            }

            return resultContainer;
        }
    }
}
