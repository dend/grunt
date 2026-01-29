// <copyright file="ModuleBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;

namespace Den.Dev.Grunt.Core.Modules
{
    /// <summary>
    /// Base class for all API modules providing shared functionality for making HTTP requests.
    /// </summary>
    public abstract class ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleBase"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        /// <param name="origin">The origin/subdomain for this module's endpoints.</param>
        protected ModuleBase(ClientBase client, string origin)
        {
            this.Client = client ?? throw new ArgumentNullException(nameof(client));
            this.Origin = origin ?? throw new ArgumentNullException(nameof(origin));
        }

        /// <summary>
        /// Gets the client instance used for executing API requests.
        /// </summary>
        protected ClientBase Client { get; }

        /// <summary>
        /// Gets the origin/subdomain for this module's API endpoints.
        /// </summary>
        protected string Origin { get; }

        /// <summary>
        /// Validates that a value is not null.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The parameter name for the exception.</param>
        /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
        protected static void ValidateNotNull<T>(T value, string paramName)
            where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Validates that an integer is within a specified range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum allowed value (inclusive).</param>
        /// <param name="max">The maximum allowed value (inclusive).</param>
        /// <param name="paramName">The parameter name for the exception.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is outside the range.</exception>
        protected static void ValidateRange(int value, int min, int max, string paramName)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Value must be between {min} and {max}.");
            }
        }

        /// <summary>
        /// Builds a full URL from a relative path using this module's origin.
        /// </summary>
        /// <param name="path">The relative path (should start with /).</param>
        /// <returns>The fully qualified URL.</returns>
        protected string BuildUrl(string path) =>
            $"https://{this.Origin}.{HaloCoreEndpoints.ServiceDomain}{path}";

        /// <summary>
        /// Executes a GET request against the API.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="useClearance">Whether to include the clearance token.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> GetAsync<T>(
            string path,
            bool useClearance = false,
            bool useSpartanToken = true) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Get,
                useSpartanToken,
                useClearance,
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a GET request against a fully specified URL.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="fullUrl">The full URL to request.</param>
        /// <param name="useClearance">Whether to include the clearance token.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <param name="customHeaders">Optional custom headers to include.</param>
        /// <param name="enforceSuccess">Whether to enforce success response codes.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> GetAsyncFullUrl<T>(
            string fullUrl,
            bool useClearance = false,
            bool useSpartanToken = true,
            List<KeyValuePair<string, string>>? customHeaders = null,
            bool enforceSuccess = true) =>
            this.Client.ExecuteAPIRequest<T>(
                fullUrl,
                HttpMethod.Get,
                useSpartanToken,
                useClearance,
                includeRawResponse: this.Client.IncludeRawResponses,
                customHeaders: customHeaders,
                enforceSuccess: enforceSuccess);

        /// <summary>
        /// Executes a POST request against the API.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="content">The JSON content to send.</param>
        /// <param name="useClearance">Whether to include the clearance token.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> PostAsync<T>(
            string path,
            string content = "",
            bool useClearance = false,
            bool useSpartanToken = true) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Post,
                useSpartanToken,
                useClearance,
                content,
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a POST request with JSON serialized body.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <typeparam name="TBody">The type of the request body.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="body">The object to serialize as JSON.</param>
        /// <param name="useClearance">Whether to include the clearance token.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <param name="contentType">The content type for the request.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> PostJsonAsync<T, TBody>(
            string path,
            TBody body,
            bool useClearance = false,
            bool useSpartanToken = true,
            APIContentType contentType = APIContentType.Json) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Post,
                useSpartanToken,
                useClearance,
                JsonSerializer.Serialize(body),
                contentType: contentType,
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a PUT request against the API.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="content">The JSON content to send.</param>
        /// <param name="useClearance">Whether to include the clearance token.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> PutAsync<T>(
            string path,
            string content = "",
            bool useClearance = false,
            bool useSpartanToken = true) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Put,
                useSpartanToken,
                useClearance,
                content,
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a PUT request with JSON serialized body.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <typeparam name="TBody">The type of the request body.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="body">The object to serialize as JSON.</param>
        /// <param name="useClearance">Whether to include the clearance token.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> PutJsonAsync<T, TBody>(
            string path,
            TBody body,
            bool useClearance = false,
            bool useSpartanToken = true) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Put,
                useSpartanToken,
                useClearance,
                JsonSerializer.Serialize(body),
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a PATCH request against the API.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="content">The JSON content to send.</param>
        /// <param name="useClearance">Whether to include the clearance token.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> PatchAsync<T>(
            string path,
            string content = "",
            bool useClearance = false,
            bool useSpartanToken = true) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Patch,
                useSpartanToken,
                useClearance,
                content,
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a PATCH request with JSON serialized body.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <typeparam name="TBody">The type of the request body.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="body">The object to serialize as JSON.</param>
        /// <param name="useClearance">Whether to include the clearance token.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> PatchJsonAsync<T, TBody>(
            string path,
            TBody body,
            bool useClearance = false,
            bool useSpartanToken = true) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Patch,
                useSpartanToken,
                useClearance,
                JsonSerializer.Serialize(body),
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a DELETE request against the API.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="useClearance">Whether to include the clearance token.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> DeleteAsync<T>(
            string path,
            bool useClearance = false,
            bool useSpartanToken = true) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Delete,
                useSpartanToken,
                useClearance,
                includeRawResponse: this.Client.IncludeRawResponses);
    }
}
