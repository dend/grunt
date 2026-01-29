// <copyright file="ClientBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Den.Dev.Grunt.Converters;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Util;
using Microsoft.Extensions.Caching.Memory;

namespace Den.Dev.Grunt.Core.Foundation
{
    /// <summary>
    /// Class containing the fundamental pieces for an API client that talks to the Halo APIs.
    /// </summary>
    public abstract class ClientBase
    {
        private const int MaxRetries = 3;
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan[] RetryDelays =
        {
            TimeSpan.FromMilliseconds(200),
            TimeSpan.FromMilliseconds(500),
            TimeSpan.FromSeconds(1),
        };

        /// <summary>
        /// Shared HttpClient instance for all ClientBase instances that don't provide their own.
        /// HttpClient is designed to be instantiated once and reused throughout the application lifecycle.
        /// </summary>
        private static readonly HttpClient SharedHttpClient = new(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,
            MaxConnectionsPerServer = 16,
        })
        {
            Timeout = DefaultTimeout,
        };

        /// <summary>
        /// Shared MemoryCache instance for all ClientBase instances.
        /// </summary>
        private static readonly MemoryCache SharedCache = new(new MemoryCacheOptions());

        private readonly MemoryCache cache;

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
        /// Initializes a new instance of the <see cref="ClientBase"/> class with the shared HttpClient and MemoryCache.
        /// </summary>
        protected ClientBase()
        {
            this.Client = SharedHttpClient;
            this.cache = SharedCache;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientBase"/> class with a custom HttpClient.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance to use for API requests.</param>
        /// <exception cref="ArgumentNullException">Thrown when httpClient is null.</exception>
        protected ClientBase(HttpClient httpClient)
        {
            this.Client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.cache = SharedCache;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientBase"/> class with a custom HttpClient and MemoryCache.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance to use for API requests.</param>
        /// <param name="memoryCache">The MemoryCache instance to use for caching responses.</param>
        /// <exception cref="ArgumentNullException">Thrown when httpClient or memoryCache is null.</exception>
        protected ClientBase(HttpClient httpClient, MemoryCache memoryCache)
        {
            this.Client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        /// <summary>
        /// Gets or sets the instance of the HTTP client that handles processing of API requests and responses.
        /// </summary>
        public HttpClient Client { get; set; }

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
        /// Gets or sets the User-Agent header value to use for outbound requests.
        /// </summary>
        public string UserAgent { get; set; } = string.Empty;

        /// <summary>
        /// Executes an API request in a standard way against a given API endpoint. This is a helper method that's put
        /// in place to simplify how the API calls are made because most requests against the Halo Infinite API are
        /// pretty repetitive.
        /// </summary>
        /// <param name="endpoint">The API endpoint to which the request is sent.</param>
        /// <param name="method">HTTP method to be used for the request.</param>
        /// <param name="useSpartanToken">Determines whether a Spartan token needs to be applied to the request.</param>
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
            HaloApiResultContainer<T, RawResponseContainer> resultContainer = new(default!, new RawResponseContainer());

            var request = this.BuildRequest(
                endpoint,
                method,
                textContent,
                binaryContent,
                contentType,
                useSpartanToken,
                useClearance,
                customHeaders);

            // Capture request details for diagnostics when includeRawResponse is enabled
            var captureRawResponse = includeRawResponse || this.IncludeRawResponses;
            if (captureRawResponse)
            {
                resultContainer.Response!.RequestUrl = endpoint;
                resultContainer.Response.RequestMethod = method.Method;
                resultContainer.Response.RequestHeaders = CaptureHeaders(request.Headers, request.Content?.Headers);
                resultContainer.Response.RequestBody = textContent;
            }

            HttpResponseMessage? response = null;
            byte[]? responseData = null;

            try
            {
                (response, responseData) = await this.SendWithCacheAsync(request, endpoint, enforceSuccess);
            }
            catch (HttpRequestException ex)
            {
                resultContainer.Response!.Message = ex.Message;

                if (ex.InnerException is WebException webException)
                {
                    if (webException.Response is HttpWebResponse httpWebResponse)
                    {
                        resultContainer.Response!.Code = (int)httpWebResponse.StatusCode;
                    }
                }
            }

            if (response != null)
            {
                resultContainer.Response!.Code = Convert.ToInt32(response!.StatusCode);

                if (captureRawResponse)
                {
                    resultContainer.Response.ResponseHeaders = CaptureHeaders(response.Headers, response.Content?.Headers);
                }

                if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotModified || enforceSuccess)
                {
                    resultContainer.Result = this.DeserializeResponse<T>(
                        responseData!,
                        response,
                        resultContainer.Response,
                        captureRawResponse);
                }

                if (response.Content != null)
                {
                    resultContainer.Response.Message = await response.Content.ReadAsStringAsync();
                }
            }

            return resultContainer;
        }

        private static bool IsTransientError(HttpResponseMessage? response, Exception? ex)
        {
            if (ex is HttpRequestException or TaskCanceledException)
            {
                return true;
            }

            if (response == null)
            {
                return false;
            }

            var code = (int)response.StatusCode;
            return code >= 500 || code == 408 || code == 429;
        }

        private static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
        {
            var clone = new HttpRequestMessage(request.Method, request.RequestUri);

            // Copy headers
            foreach (var header in request.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            // Copy content if present
            if (request.Content != null)
            {
                var contentBytes = await request.Content.ReadAsByteArrayAsync();
                clone.Content = new ByteArrayContent(contentBytes);

                // Copy content headers
                foreach (var header in request.Content.Headers)
                {
                    clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            return clone;
        }

        private static Dictionary<string, string> CaptureHeaders(HttpHeaders headers, HttpContentHeaders? contentHeaders)
        {
            var result = new Dictionary<string, string>();
            foreach (var h in headers)
            {
                result[h.Key] = string.Join(", ", h.Value);
            }

            if (contentHeaders != null)
            {
                foreach (var h in contentHeaders)
                {
                    result[h.Key] = string.Join(", ", h.Value);
                }
            }

            return result;
        }

        private async Task UpdateCacheAsync(string cacheKey, HttpResponseMessage response)
        {
            var eTag = response.Headers.ETag?.ToString();
            var content = await response.Content.ReadAsByteArrayAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60),
            };

            this.cache.Set(cacheKey, new CachedAPIResponse { ETag = eTag, Content = content }, cacheEntryOptions);
        }

        private async Task<HttpResponseMessage> SendWithRetryAsync(HttpRequestMessage request)
        {
            HttpResponseMessage? response = null;
            Exception? lastException = null;

            for (int attempt = 0; attempt <= MaxRetries; attempt++)
            {
                try
                {
                    // Clone the request for retries (HttpRequestMessage can only be sent once)
                    HttpRequestMessage requestToSend;
                    if (attempt == 0)
                    {
                        requestToSend = request;
                    }
                    else
                    {
                        requestToSend = await CloneHttpRequestMessageAsync(request);
                    }

                    response = await this.Client.SendAsync(requestToSend);

                    if (!IsTransientError(response, null))
                    {
                        return response;
                    }

                    // Transient error, will retry if attempts remain
                    lastException = null;
                }
                catch (HttpRequestException ex)
                {
                    lastException = ex;
                    if (!IsTransientError(null, ex))
                    {
                        throw;
                    }
                }
                catch (TaskCanceledException ex)
                {
                    lastException = ex;
                    if (!IsTransientError(null, ex))
                    {
                        throw;
                    }
                }

                // Don't delay after the last attempt
                if (attempt < MaxRetries)
                {
                    await Task.Delay(RetryDelays[attempt]);
                }
            }

            // If we have a response (transient error), return it
            if (response != null)
            {
                return response;
            }

            // If we have an exception, rethrow it
            if (lastException != null)
            {
                throw lastException;
            }

            // This shouldn't happen, but just in case
            throw new InvalidOperationException("Retry loop completed without response or exception");
        }

        private HttpRequestMessage BuildRequest(
            string endpoint,
            HttpMethod method,
            string textContent,
            byte[]? binaryContent,
            APIContentType contentType,
            bool useSpartanToken,
            bool useClearance,
            List<KeyValuePair<string, string>>? customHeaders)
        {
            var contentTypeAttribute = contentType.GetHeaderValue();

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(endpoint),
                Method = method,
            };

            // Request JSON responses by default, unless caller specifies a custom Accept header
            var hasCustomAccept = customHeaders?.Exists(h => h.Key.Equals("Accept", StringComparison.OrdinalIgnoreCase)) ?? false;
            if (!hasCustomAccept)
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

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

            if (!string.IsNullOrEmpty(this.UserAgent))
            {
                request.Headers.UserAgent.ParseAdd(this.UserAgent);
            }

            return request;
        }

        private async Task<(HttpResponseMessage? Response, byte[]? Data)> SendWithCacheAsync(
            HttpRequestMessage request,
            string cacheKey,
            bool enforceSuccess)
        {
            HttpResponseMessage? response = null;
            byte[]? responseData = null;

            if (this.cache.TryGetValue(cacheKey, out CachedAPIResponse? cachedResponse))
            {
                if (cachedResponse != null)
                {
                    var eTagHeader = cachedResponse.ETag;
                    request.Headers.Add("If-None-Match", eTagHeader);
                    response = await this.SendWithRetryAsync(request);

                    if (response.StatusCode == HttpStatusCode.NotModified)
                    {
                        responseData = cachedResponse.Content;
                    }
                    else
                    {
                        if (response.IsSuccessStatusCode || enforceSuccess)
                        {
                            await this.UpdateCacheAsync(cacheKey, response);
                        }

                        responseData = await response.Content.ReadAsByteArrayAsync();
                    }
                }
            }
            else
            {
                response = await this.SendWithRetryAsync(request);

                if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotModified || enforceSuccess)
                {
                    await this.UpdateCacheAsync(cacheKey, response);
                }

                responseData = await response.Content.ReadAsByteArrayAsync();
            }

            return (response, responseData);
        }

        private T? DeserializeResponse<T>(
            byte[] responseData,
            HttpResponseMessage response,
            RawResponseContainer rawResponse,
            bool captureRaw)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(Encoding.UTF8.GetString(responseData), typeof(T));
            }

            if (typeof(T) == typeof(byte[]))
            {
                return (T)Convert.ChangeType(responseData, typeof(T));
            }

            if (typeof(T) == typeof(bool))
            {
                return (T)(object)response.IsSuccessStatusCode;
            }

            // Check whether the type is either one of the supported types or is a generic type
            if (Attribute.GetCustomAttribute(typeof(T), typeof(IsAutomaticallySerializableAttribute)) != null ||
                typeof(T).IsGenericType)
            {
                var responseString = Encoding.UTF8.GetString(responseData);
                if (!string.IsNullOrWhiteSpace(responseString))
                {
                    if (captureRaw)
                    {
                        rawResponse.Message = responseString;
                    }

                    try
                    {
                        return JsonSerializer.Deserialize<T>(responseString, this.serializerOptions);
                    }
                    catch (JsonException)
                    {
                        // Deserialization failed, but HTTP details are preserved in Response
                        return default;
                    }
                }
            }
            else
            {
                throw new NotSupportedException("The specified type is not supported. You can only get results in string or byte array formats.");
            }

            return default;
        }
    }
}
