// <copyright file="WaypointModuleBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Util;

namespace Den.Dev.Grunt.Core.Modules.Waypoint
{
    /// <summary>
    /// Base class for all Waypoint API modules providing shared functionality for making HTTP requests.
    /// </summary>
    public abstract class WaypointModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaypointModuleBase"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        /// <param name="origin">The origin/subdomain for this module's endpoints.</param>
        protected WaypointModuleBase(ClientBase client, string origin)
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
        /// Builds a full URL from a relative path using this module's origin.
        /// </summary>
        /// <param name="path">The relative path (should start with /).</param>
        /// <returns>The fully qualified URL.</returns>
        protected string BuildUrl(string path) =>
            $"https://{this.Origin}.{WaypointEndpoints.ServiceDomain}{path}";

        /// <summary>
        /// Executes a GET request against the API.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to false.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> GetAsync<T>(
            string path,
            bool useSpartanToken = false) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Get,
                useSpartanToken,
                useClearance: false,
                textContent: GlobalConstants.WEB_USER_AGENT,
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a GET request against a fully specified URL.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="fullUrl">The full URL to request.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to false.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> GetAsyncFullUrl<T>(
            string fullUrl,
            bool useSpartanToken = false) =>
            this.Client.ExecuteAPIRequest<T>(
                fullUrl,
                HttpMethod.Get,
                useSpartanToken,
                useClearance: false,
                textContent: GlobalConstants.WEB_USER_AGENT,
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a POST request against the API with WEB_USER_AGENT content.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> PostAsync<T>(
            string path,
            bool useSpartanToken = true) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Post,
                useSpartanToken,
                useClearance: false,
                textContent: GlobalConstants.WEB_USER_AGENT,
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a POST request with JSON serialized body.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <typeparam name="TBody">The type of the request body.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="body">The object to serialize as JSON.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> PostJsonAsync<T, TBody>(
            string path,
            TBody body,
            bool useSpartanToken = true) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Post,
                useSpartanToken,
                useClearance: false,
                textContent: JsonSerializer.Serialize(body),
                includeRawResponse: this.Client.IncludeRawResponses);

        /// <summary>
        /// Executes a PUT request with JSON serialized body.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <typeparam name="TBody">The type of the request body.</typeparam>
        /// <param name="path">The relative API path.</param>
        /// <param name="body">The object to serialize as JSON.</param>
        /// <param name="useSpartanToken">Whether to include the Spartan token. Defaults to true.</param>
        /// <returns>The API response container.</returns>
        protected Task<HaloApiResultContainer<T, RawResponseContainer>> PutJsonAsync<T, TBody>(
            string path,
            TBody body,
            bool useSpartanToken = true) =>
            this.Client.ExecuteAPIRequest<T>(
                this.BuildUrl(path),
                HttpMethod.Put,
                useSpartanToken,
                useClearance: false,
                textContent: JsonSerializer.Serialize(body),
                contentType: APIContentType.Json,
                includeRawResponse: this.Client.IncludeRawResponses);
    }
}
