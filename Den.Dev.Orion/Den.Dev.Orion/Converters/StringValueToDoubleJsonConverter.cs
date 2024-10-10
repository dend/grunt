// <copyright file="StringValueToDoubleJsonConverter.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Den.Dev.Orion.Converters
{
    /// <summary>
    /// Ensures that any broken double values that can be strings are actually passed as doubles.
    /// </summary>
    /// <remarks>
    /// This originates from the call to /hi/matches/MATCH_ID/skill where there's a chance that the expected number or the standard deviation can be "NaN".
    /// </remarks>
    public class StringValueToDoubleJsonConverter : JsonConverter<double?>
    {
        /// <summary>
        /// Read content from the JSON parser.
        /// </summary>
        /// <param name="reader">Instance of <see cref="Utf8JsonReader"/> used to read the JSON content.</param>
        /// <param name="typeToConvert">JSON data to convert.</param>
        /// <param name="options">JSON serialization options.</param>
        /// <returns>If successful, returns an instance of <see cref="double"/> containing the date and time. Otherwise, returns null.</returns>
        public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            double? result;

            try
            {
                result = reader.GetDouble();
            }
            catch 
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Writes content through a JSON parser.
        /// </summary>
        /// <param name="writer">Instance of <see cref="Utf8JsonWriter"/> that will be writing the JSON data.</param>
        /// <param name="value">Instance of <see cref="double"/> containing the date and time to be written into JSON.</param>
        /// <param name="options">JSON serialization options.</param>
        public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
