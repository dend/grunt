// <copyright file="SingleOrArrayJsonConverter.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Converters
{
    /// <summary>
    /// Ensures that objects are consistently cast as an array even if only a single object is passed.
    /// </summary>
    /// <typeparam name="T">Type to be cast to.</typeparam>
    internal class SingleOrArrayJsonConverter<T> : JsonConverter<List<T>>
    {
        /// <summary>
        /// Determines whether the type can be properly converted to the target type.
        /// </summary>
        /// <param name="typeToConvert">Type of the entity to be converted.</param>
        /// <returns>If the entity can be converted, returns "true". Otherwise, returns "false".</returns>
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(List<T>);
        }

        /// <summary>
        /// Read the entity.
        /// </summary>
        /// <param name="reader">JSON reader.</param>
        /// <param name="typeToConvert">Type that needs to be converted.</param>
        /// <param name="options">Additional reading options.</param>
        /// <returns>If successful, returns a <see cref="List{T}"/> instance.</returns>
        public override List<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                using JsonDocument jsonDoc = JsonDocument.ParseValue(ref reader);
                return jsonDoc.Deserialize<List<T>>();
            }
            else
            {
                using JsonDocument jsonDoc = JsonDocument.ParseValue(ref reader);
                return jsonDoc != null ? new List<T> { jsonDoc.Deserialize<T>()! } : new List<T>();
            }
        }

        /// <summary>
        /// Writes the target type data to a writer.
        /// </summary>
        /// <remarks>
        /// Not used. Will throw a <see cref="NotImplementedException"/> at this time.
        /// </remarks>
        /// <param name="writer">JSON writer.</param>
        /// <param name="value">Instance of <see cref="List{T}"/> that needs to be written.</param>
        /// <param name="options">Additional writing options.</param>
        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
