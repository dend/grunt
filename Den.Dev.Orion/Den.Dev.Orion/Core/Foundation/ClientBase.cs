// <copyright file="ClientBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Den.Dev.Orion.Converters;
using Den.Dev.Orion.Models;
using Den.Dev.Orion.Util;
using Microsoft.Extensions.Caching.Memory;

namespace Den.Dev.Orion.Core.Foundation
{
    /// <summary>
    /// Class containing the fundamental pieces for an API client that talks to the Halo APIs.
    /// </summary>
    public abstract class ClientBase
    {
        private readonly MemoryCache cache = new(new MemoryCacheOptions());

        private readonly JsonSerializerOptions serializerOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new EmptyDateStringToNullJsonConverter(),
                new XmlDurationToTimeSpanJsonConverter(),
                new StringValueToDoubleJsonConverter(),
            },
        };

        /// <summary>
        /// Gets or sets the instance of the HTTP client that handles processing of API requests and responses.
        /// </summary>
        public HttpClient Client { get; set; } = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,
            MaxConnectionsPerServer = 16,
        });

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
        /// Gets or sets a value indicating whether to include the raw JSON responses with each function call.
        /// </summary>
        public bool IncludeRawResponses { get; set; } = false;

        /// <summary>
        /// Executes an API request in a standard way against a given API endpoint. This is a helper method that's put
        /// in place to simplify how the API calls are made because most requests against the Halo Infinite API are
        /// pretty repetitive.
        /// </summary>
        /// <param name="endpoint">The API endpoint to which the request is sent.</param>
        /// <param name="method">HTTP method to be used for the request.</param>
        /// <param name="useSpartanToken">Determines whether a Spartan token needs to be applied to teh request.</param>
        /// <param name="useClearance">Determines whether a clearance/flight ID needs to be applied to the request.</param>
        /// <param name="textContent">If the request contains data to be sent to the Halo Waypoint service, include it here. Expected format is JSON.</param>
        /// <param name="binaryContent">Binary content to be passed to the API. Either this or textContent should be used, but not both. Binary content takes priority.</param>
        /// <param name="contentType">Content type for POST requests. By default it's `application/json`.</param>
        /// <param name="includeRawResponse">Determines whether a raw response will be returned with the result. Disabled by default.</param>
        /// <param name="customHeaders">A list of custom headers to append to the request.</param>
        /// <param name="enforceSuccess">Determines whether to try and serialize the response data even if the request returns an error code (that is - not HTTP 200 OK). Default is set to true.</param>
        /// <typeparam name="T">Data type to return with the response metadata.</typeparam>
        /// <returns>Response string in case of a successful request. Null if request failed.</returns>
        public async Task<HaloApiResultContainer<T, RawResponseContainer>> ExecuteAPIRequest<T>(
            string endpoint,
            HttpMethod method,
            bool useSpartanToken,
            bool useClearance,
            string textContent = "",
            byte[]? binaryContent = null,
            APIContentType contentType = APIContentType.Json,
            bool includeRawResponse = false,
            List<KeyValuePair<string, string>>? customHeaders = null,
            bool enforceSuccess = true)
        {
            var contentTypeAttribute = contentType.GetHeaderValue();

            HaloApiResultContainer<T, RawResponseContainer> resultContainer = new(default!, new RawResponseContainer());

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(endpoint),
                Method = method,
            };
            
            if (!string.IsNullOrEmpty(textContent))
            {
                request.Content = new StringContent(textContent, Encoding.UTF8, contentTypeAttribute!);
            }

            if (binaryContent != null)
            {
                request.Content = new ByteArrayContent(binaryContent);
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

            if (customHeaders != null)
            {
                foreach (var header in customHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage? response = null;
            byte[]? responseData = null;

            try
            {
                if (this.cache.TryGetValue(endpoint, out CachedAPIResponse? cachedResponse))
                {
                    if (cachedResponse != null)
                    {
                        var eTagHeader = cachedResponse.ETag;
                        request.Headers.Add("If-None-Match", eTagHeader);
                        response = await this.Client.SendAsync(request);

                        if (response.StatusCode == HttpStatusCode.NotModified)
                        {
                            responseData = cachedResponse.Content;
                        }
                        else
                        {
                            // We only want to update cache if the request is successful
                            // or the developer explicitly wants to enforce a successful response
                            // policy (which means that even error codes are considered success)
                            if (response.IsSuccessStatusCode || enforceSuccess)
                            {
                                this.UpdateCache(endpoint, response);
                            }

                            responseData = await response.Content.ReadAsByteArrayAsync();
                        }
                    }
                }
                else
                {
                    response = await this.Client.SendAsync(request);

                    if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotModified || enforceSuccess)
                    {
                        this.UpdateCache(endpoint, response);
                    }

                    responseData = await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                resultContainer.Response!.Message = ex.Message;

                if (ex.InnerException is WebException webException)
                {
                    if (webException.Response is HttpWebResponse httpWebResponse)
                    {
                        // Extract HTTP status code from the response
                        resultContainer.Response!.Code = (int)httpWebResponse.StatusCode;
                    }
                }
            }

            if (response != null)
            {
                resultContainer.Response!.Code = Convert.ToInt32(response!.StatusCode);

                if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotModified || enforceSuccess)
                {
                    if (typeof(T) == typeof(string))
                    {
                        resultContainer.Result = (T)Convert.ChangeType(Encoding.UTF8.GetString(responseData!), typeof(T));
                    }
                    else if (typeof(T) == typeof(byte[]))
                    {
                        resultContainer.Result = (T)Convert.ChangeType(responseData!, typeof(T));
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
                            var responseString = Encoding.UTF8.GetString(responseData!);
                            if (!string.IsNullOrWhiteSpace(responseString))
                            {
                                resultContainer.Result = JsonSerializer.Deserialize<T>(responseString, this.serializerOptions);
                                if (includeRawResponse)
                                {
                                    resultContainer.Response.Message = responseString;
                                }
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
                    resultContainer.Response.Message = await response.Content.ReadAsStringAsync();
                }
            }

            return resultContainer;
        }

        private void UpdateCache(string cacheKey, HttpResponseMessage response)
        {
            var eTag = response.Headers.ETag?.ToString();
            var content = response.Content.ReadAsByteArrayAsync().Result;

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60),
            };

            this.cache.Set(cacheKey, new CachedAPIResponse { ETag = eTag, Content = content }, cacheEntryOptions);
        }
    }
}
