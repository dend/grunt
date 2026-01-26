// <copyright file="EndpointExecutor.cs" company="Den Delimarsky">
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
using System.Threading.Tasks;
using Den.Dev.Grunt.Core;
using Den.Dev.Grunt.Models;

namespace Den.Dev.Grunt.Auditor.Services
{
    /// <summary>
    /// Executes API endpoints using reflection to call HaloInfiniteClient methods.
    /// </summary>
    public class EndpointExecutor
    {
        private readonly HaloInfiniteClient _client;
        private readonly ParameterRegistry _registry;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointExecutor"/> class.
        /// </summary>
        /// <param name="client">Authenticated HaloInfiniteClient.</param>
        /// <param name="registry">Parameter registry for resolving arguments.</param>
        public EndpointExecutor(HaloInfiniteClient client, ParameterRegistry registry)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        }

        /// <summary>
        /// Executes an endpoint method and returns the result.
        /// </summary>
        /// <param name="methodPath">Method path (e.g., "Stats.GetMatchHistory").</param>
        /// <param name="args">Arguments to pass to the method.</param>
        /// <returns>Execution result containing typed result, raw JSON, and metadata.</returns>
        public async Task<ExecutionResult> ExecuteAsync(string methodPath, Dictionary<string, object> args)
        {
            var result = new ExecutionResult();

            try
            {
                // Parse method path (e.g., "Stats.GetMatchHistory" -> module "Stats", method "GetMatchHistory")
                var parts = methodPath.Split('.');
                if (parts.Length != 2)
                {
                    result.ErrorMessage = $"Invalid method path: {methodPath}. Expected format: Module.Method";
                    return result;
                }

                var moduleName = parts[0];
                var methodName = parts[1];

                // Get the module property from the client
                var moduleProperty = _client.GetType().GetProperty(moduleName);
                if (moduleProperty == null)
                {
                    result.ErrorMessage = $"Module not found: {moduleName}";
                    return result;
                }

                var module = moduleProperty.GetValue(_client);
                if (module == null)
                {
                    result.ErrorMessage = $"Module {moduleName} is null";
                    return result;
                }

                // Resolve argument references
                var resolvedArgs = _registry.ResolveArguments(args);

                // Find the method
                var methods = module.GetType().GetMethods()
                    .Where(m => m.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (methods.Count == 0)
                {
                    result.ErrorMessage = $"Method not found: {methodName} in module {moduleName}";
                    return result;
                }

                // Find best matching method based on parameters
                MethodInfo? bestMethod = null;
                object[]? methodArgs = null;

                foreach (var method in methods)
                {
                    var parameters = method.GetParameters();
                    var matchedArgs = TryMatchArguments(parameters, resolvedArgs);

                    if (matchedArgs != null)
                    {
                        bestMethod = method;
                        methodArgs = matchedArgs;
                        break;
                    }
                }

                if (bestMethod == null || methodArgs == null)
                {
                    result.ErrorMessage = $"Could not match arguments for {moduleName}.{methodName}. " +
                        $"Available: {string.Join(", ", resolvedArgs.Keys)}";
                    return result;
                }

                // Invoke the method
                var invokeResult = bestMethod.Invoke(module, methodArgs);

                if (invokeResult is Task task)
                {
                    await task;

                    // Get the result from the task
                    var taskType = task.GetType();
                    if (taskType.IsGenericType)
                    {
                        var resultProperty = taskType.GetProperty("Result");
                        var taskResult = resultProperty?.GetValue(task);

                        if (taskResult != null)
                        {
                            result.TypedResult = taskResult;

                            // Extract raw JSON and request details from RawResponseContainer if available
                            var responseProperty = taskResult.GetType().GetProperty("Response");
                            if (responseProperty != null)
                            {
                                var response = responseProperty.GetValue(taskResult);
                                if (response is RawResponseContainer rawContainer)
                                {
                                    result.RawJson = rawContainer.Message;
                                    result.HttpStatusCode = rawContainer.Code;
                                    result.RequestUrl = rawContainer.RequestUrl;
                                    result.RequestMethod = rawContainer.RequestMethod;
                                    result.RequestHeaders = rawContainer.RequestHeaders;
                                }
                            }

                            // Get the Result property for the typed data
                            var dataProperty = taskResult.GetType().GetProperty("Result");
                            if (dataProperty != null)
                            {
                                result.TypedResult = dataProperty.GetValue(taskResult);
                            }

                            result.Success = true;
                        }
                    }
                }
            }
            catch (TargetInvocationException ex)
            {
                result.ErrorMessage = ex.InnerException?.Message ?? ex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Tries to match provided arguments to method parameters.
        /// </summary>
        private object[]? TryMatchArguments(ParameterInfo[] parameters, Dictionary<string, object> args)
        {
            var result = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                var paramName = param.Name ?? string.Empty;

                if (args.TryGetValue(paramName, out var value))
                {
                    // Try to convert the value to the parameter type
                    var converted = ConvertValue(value, param.ParameterType);
                    if (converted != null)
                    {
                        result[i] = converted;
                    }
                    else if (param.HasDefaultValue)
                    {
                        result[i] = param.DefaultValue!;
                    }
                    else
                    {
                        return null; // Required parameter couldn't be converted
                    }
                }
                else if (param.HasDefaultValue)
                {
                    result[i] = param.DefaultValue!;
                }
                else
                {
                    return null; // Required parameter not provided
                }
            }

            return result;
        }

        /// <summary>
        /// Converts a value to the target type.
        /// </summary>
        private object? ConvertValue(object value, Type targetType)
        {
            if (value == null)
            {
                return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
            }

            var valueType = value.GetType();

            // Handle nullable types
            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            // Direct assignment if types match
            if (underlyingType.IsAssignableFrom(valueType))
            {
                return value;
            }

            // String conversions
            if (value is string strValue)
            {
                if (underlyingType == typeof(int) && int.TryParse(strValue, out var intVal))
                {
                    return intVal;
                }

                if (underlyingType == typeof(long) && long.TryParse(strValue, out var longVal))
                {
                    return longVal;
                }

                if (underlyingType == typeof(bool) && bool.TryParse(strValue, out var boolVal))
                {
                    return boolVal;
                }

                if (underlyingType == typeof(Guid) && Guid.TryParse(strValue, out var guidVal))
                {
                    return guidVal;
                }

                if (underlyingType.IsEnum)
                {
                    if (Enum.TryParse(underlyingType, strValue, true, out var enumVal))
                    {
                        return enumVal;
                    }
                }

                // String target
                if (underlyingType == typeof(string))
                {
                    return strValue;
                }
            }

            // Numeric conversions
            if (IsNumericType(valueType) && IsNumericType(underlyingType))
            {
                return Convert.ChangeType(value, underlyingType);
            }

            // JsonElement handling
            if (value is JsonElement element)
            {
                return ConvertJsonElement(element, underlyingType);
            }

            return null;
        }

        private object? ConvertJsonElement(JsonElement element, Type targetType)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.String:
                    var str = element.GetString();
                    return ConvertValue(str ?? string.Empty, targetType);

                case JsonValueKind.Number:
                    if (targetType == typeof(int))
                    {
                        return element.GetInt32();
                    }

                    if (targetType == typeof(long))
                    {
                        return element.GetInt64();
                    }

                    if (targetType == typeof(double))
                    {
                        return element.GetDouble();
                    }

                    return element.GetInt32();

                case JsonValueKind.True:
                    return true;

                case JsonValueKind.False:
                    return false;

                default:
                    return null;
            }
        }

        private static bool IsNumericType(Type type)
        {
            return type == typeof(int) ||
                   type == typeof(long) ||
                   type == typeof(short) ||
                   type == typeof(byte) ||
                   type == typeof(double) ||
                   type == typeof(float) ||
                   type == typeof(decimal);
        }
    }

    /// <summary>
    /// Result of an endpoint execution.
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the execution succeeded.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the typed result object.
        /// </summary>
        public object? TypedResult { get; set; }

        /// <summary>
        /// Gets or sets the raw JSON response.
        /// </summary>
        public string? RawJson { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the error message if execution failed.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the full URL of the HTTP request.
        /// </summary>
        public string? RequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method used for the request.
        /// </summary>
        public string? RequestMethod { get; set; }

        /// <summary>
        /// Gets or sets the HTTP headers sent with the request.
        /// </summary>
        public Dictionary<string, string>? RequestHeaders { get; set; }
    }
}
