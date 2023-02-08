// <copyright file="EmptyDateStringToNullJsonConverter.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Converters
{
    /// <summary>
    /// Converts an empty date string to a null. 343i is returning some ISO8601 dates that we wrap in <see cref="Models.APIFormattedDate"/> as empty,
    /// which in turn breaks System.Text.Json deserialization.
    /// </summary>
    internal class EmptyDateStringToNullJsonConverter : JsonConverter<DateTime?>
    {
        /// <summary>
        /// Read content from the JSON parser.
        /// </summary>
        /// <param name="reader">Instance of <see cref="Utf8JsonReader"/> used to read the JSON content.</param>
        /// <param name="typeToConvert">JSON data to convert.</param>
        /// <param name="options">JSON serialization options.</param>
        /// <returns>If successful, returns an instance of <see cref="DateTime"/> containing the date and time. Otherwise, returns null.</returns>
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            return string.IsNullOrWhiteSpace(value) ? null : DateTime.Parse(value);
        }

        /// <summary>
        /// Writes content through a JSON parser.
        /// </summary>
        /// <param name="writer">Instance of <see cref="Utf8JsonWriter"/> that will be writing the JSON data.</param>
        /// <param name="value">Instance of <see cref="DateTime"/> containing the date and time to be written into JSON.</param>
        /// <param name="options">JSON serialization options.</param>
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
