// <copyright file="ResponseValidator.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Den.Dev.Grunt.Auditor.Models;

namespace Den.Dev.Grunt.Auditor.Services
{
    /// <summary>
    /// Validates JSON responses against C# model structures.
    /// </summary>
    public class ResponseValidator
    {
        private readonly Dictionary<string, Type> _modelTypeCache = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseValidator"/> class.
        /// </summary>
        public ResponseValidator()
        {
            // Pre-cache model types from the HaloInfinite models namespace
            CacheModelTypes();
        }

        /// <summary>
        /// Validates a JSON response against a model type.
        /// </summary>
        /// <param name="rawJson">Raw JSON response string.</param>
        /// <param name="modelTypeName">Name of the C# model type.</param>
        /// <returns>List of discrepancies found.</returns>
        public List<FieldDiscrepancy> Validate(string rawJson, string modelTypeName)
        {
            var discrepancies = new List<FieldDiscrepancy>();

            if (string.IsNullOrEmpty(rawJson))
            {
                discrepancies.Add(new FieldDiscrepancy
                {
                    Type = DiscrepancyType.DeserializationFailure,
                    Path = "$",
                    Message = "Raw JSON is null or empty",
                });
                return discrepancies;
            }

            // Find the model type
            var modelType = ResolveModelType(modelTypeName);
            if (modelType == null)
            {
                discrepancies.Add(new FieldDiscrepancy
                {
                    Type = DiscrepancyType.DeserializationFailure,
                    Path = "$",
                    Message = $"Model type not found: {modelTypeName}",
                });
                return discrepancies;
            }

            try
            {
                using var doc = JsonDocument.Parse(rawJson);
                ValidateElement(doc.RootElement, modelType, "$", discrepancies);
            }
            catch (JsonException ex)
            {
                discrepancies.Add(new FieldDiscrepancy
                {
                    Type = DiscrepancyType.DeserializationFailure,
                    Path = "$",
                    Message = $"JSON parse error: {ex.Message}",
                });
            }

            return discrepancies;
        }

        /// <summary>
        /// Validates a JSON response against a specific Type.
        /// </summary>
        /// <param name="rawJson">Raw JSON response string.</param>
        /// <param name="modelType">The C# model type.</param>
        /// <returns>List of discrepancies found.</returns>
        public List<FieldDiscrepancy> Validate(string rawJson, Type modelType)
        {
            var discrepancies = new List<FieldDiscrepancy>();

            if (string.IsNullOrEmpty(rawJson))
            {
                discrepancies.Add(new FieldDiscrepancy
                {
                    Type = DiscrepancyType.DeserializationFailure,
                    Path = "$",
                    Message = "Raw JSON is null or empty",
                });
                return discrepancies;
            }

            try
            {
                using var doc = JsonDocument.Parse(rawJson);
                ValidateElement(doc.RootElement, modelType, "$", discrepancies);
            }
            catch (JsonException ex)
            {
                discrepancies.Add(new FieldDiscrepancy
                {
                    Type = DiscrepancyType.DeserializationFailure,
                    Path = "$",
                    Message = $"JSON parse error: {ex.Message}",
                });
            }

            return discrepancies;
        }

        /// <summary>
        /// Resolves a model type name to its Type.
        /// </summary>
        public Type? ResolveModelType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }

            // Handle generic types like "List<Server>"
            if (typeName.StartsWith("List<") && typeName.EndsWith(">"))
            {
                var innerTypeName = typeName.Substring(5, typeName.Length - 6);
                var innerType = ResolveModelType(innerTypeName);
                if (innerType != null)
                {
                    return typeof(List<>).MakeGenericType(innerType);
                }

                return null;
            }

            // Handle Dictionary types
            if (typeName.StartsWith("Dictionary<"))
            {
                // For now, return a generic dictionary type
                return typeof(Dictionary<string, object>);
            }

            // Check cache
            if (_modelTypeCache.TryGetValue(typeName, out var cachedType))
            {
                return cachedType;
            }

            // Try to find in the models namespace
            var assembly = typeof(Den.Dev.Grunt.Models.HaloInfinite.Achievement).Assembly;
            var fullTypeName = $"Den.Dev.Grunt.Models.HaloInfinite.{typeName}";
            var type = assembly.GetType(fullTypeName);

            if (type != null)
            {
                _modelTypeCache[typeName] = type;
            }

            return type;
        }

        private void CacheModelTypes()
        {
            var assembly = typeof(Den.Dev.Grunt.Models.HaloInfinite.Achievement).Assembly;
            var types = assembly.GetTypes()
                .Where(t => t.Namespace == "Den.Dev.Grunt.Models.HaloInfinite" && t.IsClass);

            foreach (var type in types)
            {
                _modelTypeCache[type.Name] = type;
            }
        }

        private void ValidateElement(JsonElement element, Type expectedType, string path, List<FieldDiscrepancy> discrepancies)
        {
            // Handle nullable types
            var underlyingType = Nullable.GetUnderlyingType(expectedType) ?? expectedType;

            // Handle null values
            if (element.ValueKind == JsonValueKind.Null)
            {
                if (underlyingType.IsValueType && Nullable.GetUnderlyingType(expectedType) == null)
                {
                    discrepancies.Add(new FieldDiscrepancy
                    {
                        Type = DiscrepancyType.NullabilityIssue,
                        Path = path,
                        ExpectedType = expectedType.Name,
                        JsonType = "null",
                        Message = $"Null value for non-nullable type {expectedType.Name}",
                    });
                }

                return;
            }

            // Handle arrays/lists
            if (element.ValueKind == JsonValueKind.Array)
            {
                if (IsCollectionType(underlyingType))
                {
                    var elementType = GetCollectionElementType(underlyingType);
                    if (elementType != null)
                    {
                        var index = 0;
                        foreach (var item in element.EnumerateArray())
                        {
                            ValidateElement(item, elementType, $"{path}[{index}]", discrepancies);
                            index++;
                        }
                    }
                }
                else
                {
                    discrepancies.Add(new FieldDiscrepancy
                    {
                        Type = DiscrepancyType.TypeMismatch,
                        Path = path,
                        ExpectedType = expectedType.Name,
                        JsonType = "array",
                        Message = $"Expected {expectedType.Name}, got array with {element.GetArrayLength()} elements",
                        ActualValue = GetFullJson(element),
                    });
                }

                return;
            }

            // Handle objects
            if (element.ValueKind == JsonValueKind.Object)
            {
                if (underlyingType.IsPrimitive || underlyingType == typeof(string))
                {
                    discrepancies.Add(new FieldDiscrepancy
                    {
                        Type = DiscrepancyType.TypeMismatch,
                        Path = path,
                        ExpectedType = expectedType.Name,
                        JsonType = "object",
                        Message = $"Expected {expectedType.Name}, got object",
                    });
                    return;
                }

                // Handle Dictionary types
                if (IsDictionaryType(underlyingType))
                {
                    // Dictionaries can have arbitrary keys, so we don't validate structure
                    return;
                }

                // Get expected properties from the model
                var modelProperties = GetModelProperties(underlyingType);
                var jsonProperties = new HashSet<string>(element.EnumerateObject().Select(p => p.Name), StringComparer.OrdinalIgnoreCase);

                // Check for unexpected properties (JSON has but model doesn't)
                foreach (var prop in element.EnumerateObject())
                {
                    if (!modelProperties.TryGetValue(prop.Name, out var modelProperty))
                    {
                        // Check case-insensitive match
                        var matchingProp = modelProperties.FirstOrDefault(mp =>
                            mp.Key.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));

                        if (matchingProp.Value == null)
                        {
                            discrepancies.Add(new FieldDiscrepancy
                            {
                                Type = DiscrepancyType.UnexpectedProperty,
                                Path = $"{path}.{prop.Name}",
                                JsonType = prop.Value.ValueKind.ToString(),
                                Message = $"Property '{prop.Name}' exists in JSON but not in model {underlyingType.Name}",
                                ActualValue = GetFullJson(prop.Value),
                            });
                        }
                        else
                        {
                            // Found case-insensitive match, validate it
                            ValidateElement(prop.Value, matchingProp.Value.PropertyType, $"{path}.{prop.Name}", discrepancies);
                        }
                    }
                    else
                    {
                        // Validate the property
                        ValidateElement(prop.Value, modelProperty.PropertyType, $"{path}.{prop.Name}", discrepancies);
                    }
                }

                return;
            }

            // Handle primitive types
            ValidatePrimitiveType(element, underlyingType, path, discrepancies);
        }

        private void ValidatePrimitiveType(JsonElement element, Type expectedType, string path, List<FieldDiscrepancy> discrepancies)
        {
            var jsonType = element.ValueKind.ToString();
            var isCompatible = expectedType switch
            {
                // String types
                Type t when t == typeof(string) => element.ValueKind == JsonValueKind.String,

                // Numeric types
                Type t when t == typeof(int) || t == typeof(long) || t == typeof(short) ||
                           t == typeof(byte) || t == typeof(double) || t == typeof(float) ||
                           t == typeof(decimal) => element.ValueKind == JsonValueKind.Number,

                // Boolean
                Type t when t == typeof(bool) => element.ValueKind == JsonValueKind.True ||
                                                 element.ValueKind == JsonValueKind.False,

                // DateTime - can be string in JSON
                Type t when t == typeof(DateTime) || t == typeof(DateTimeOffset) =>
                    element.ValueKind == JsonValueKind.String,

                // TimeSpan - can be string in JSON (XML duration format)
                Type t when t == typeof(TimeSpan) => element.ValueKind == JsonValueKind.String,

                // Guid - string in JSON
                Type t when t == typeof(Guid) => element.ValueKind == JsonValueKind.String,

                // Enum - can be string or number
                Type t when t.IsEnum => element.ValueKind == JsonValueKind.String ||
                                        element.ValueKind == JsonValueKind.Number,

                // Default - assume compatible if not object/array mismatch
                _ => true,
            };

            if (!isCompatible)
            {
                discrepancies.Add(new FieldDiscrepancy
                {
                    Type = DiscrepancyType.TypeMismatch,
                    Path = path,
                    ExpectedType = expectedType.Name,
                    JsonType = jsonType,
                    Message = $"Type mismatch: expected {expectedType.Name}, got JSON {jsonType}",
                    ActualValue = GetFullJson(element),
                });
            }
        }

        private Dictionary<string, PropertyInfo> GetModelProperties(Type type)
        {
            var result = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                // Check for JsonPropertyName attribute
                var jsonAttr = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
                var name = jsonAttr?.Name ?? prop.Name;

                result[name] = prop;
            }

            return result;
        }

        private bool IsCollectionType(Type type)
        {
            if (type.IsArray)
            {
                return true;
            }

            if (type.IsGenericType)
            {
                var genericDef = type.GetGenericTypeDefinition();
                return genericDef == typeof(List<>) ||
                       genericDef == typeof(IList<>) ||
                       genericDef == typeof(IEnumerable<>) ||
                       genericDef == typeof(ICollection<>);
            }

            return false;
        }

        private bool IsDictionaryType(Type type)
        {
            if (type.IsGenericType)
            {
                var genericDef = type.GetGenericTypeDefinition();
                return genericDef == typeof(Dictionary<,>) ||
                       genericDef == typeof(IDictionary<,>);
            }

            return false;
        }

        private Type? GetCollectionElementType(Type type)
        {
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            if (type.IsGenericType)
            {
                return type.GetGenericArguments().FirstOrDefault();
            }

            return null;
        }

        private string GetFullJson(JsonElement element)
        {
            return element.GetRawText();
        }
    }
}
