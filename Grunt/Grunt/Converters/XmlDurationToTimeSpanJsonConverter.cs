// <copyright file="XmlDurationToTimeSpanJsonConverter.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace OpenSpartan.Grunt.Converters
{
    /// <summary>
    /// Helper class used for conversion of XML duration into standard time span.
    /// </summary>
    internal class XmlDurationToTimeSpanJsonConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// Read the XML duration.
        /// </summary>
        /// <param name="reader">JSON reader.</param>
        /// <param name="typeToConvert">Type that needs to be converted.</param>
        /// <param name="options">Additional reading options.</param>
        /// <returns>If successful, returns a <see cref="TimeSpan"/> instance.</returns>
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            return string.IsNullOrWhiteSpace(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
        }

        /// <summary>
        /// Writes the TimeSpan data to a pre-defined writer.
        /// </summary>
        /// <param name="writer">JSON writer.</param>
        /// <param name="value">Instance of <see cref="TimeSpan"/> that needs to be written.</param>
        /// <param name="options">Additional writing options.</param>
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(XmlConvert.ToString(value));
        }
    }
}
