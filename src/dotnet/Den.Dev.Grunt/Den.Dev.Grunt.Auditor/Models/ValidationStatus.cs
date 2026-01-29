// <copyright file="ValidationStatus.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Auditor.Models
{
    /// <summary>
    /// Status of an endpoint validation result.
    /// </summary>
    public enum ValidationStatus
    {
        /// <summary>
        /// Validation passed with no issues.
        /// </summary>
        Pass,

        /// <summary>
        /// Validation passed with warnings (e.g., unexpected properties that don't cause data loss).
        /// </summary>
        Warning,

        /// <summary>
        /// Validation failed (e.g., type mismatches, deserialization failures).
        /// </summary>
        Fail,

        /// <summary>
        /// Endpoint was skipped (e.g., destructive operation, missing parameters).
        /// </summary>
        Skipped,

        /// <summary>
        /// API call failed (network error, authentication error, etc.).
        /// </summary>
        Error,
    }
}
