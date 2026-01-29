// <copyright file="DiscrepancyType.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Auditor.Models
{
    /// <summary>
    /// Types of discrepancies that can be found during validation.
    /// </summary>
    public enum DiscrepancyType
    {
        /// <summary>
        /// A property exists in the JSON response but not in the C# model.
        /// This represents potential data loss.
        /// </summary>
        UnexpectedProperty,

        /// <summary>
        /// A property exists in the C# model but not in the JSON response.
        /// This may be expected for optional properties.
        /// </summary>
        MissingProperty,

        /// <summary>
        /// The JSON type does not match the expected C# type.
        /// </summary>
        TypeMismatch,

        /// <summary>
        /// A null value was found where a non-nullable type was expected.
        /// </summary>
        NullabilityIssue,

        /// <summary>
        /// Deserialization failed entirely for this response.
        /// </summary>
        DeserializationFailure,

        /// <summary>
        /// A custom converter had to handle an edge case.
        /// </summary>
        DeserializationWarning,
    }
}
