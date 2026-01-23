using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Den.Dev.Grunt.Zeta.Models;
using Den.Dev.Grunt.Zeta.UI;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.Services
{
    public class ApiExecutionService
    {
        private readonly HistoryService _historyService;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

        public ApiExecutionService(HistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<ApiCallRecord> ExecuteMethodAsync(
            ModuleMetadata module,
            MethodMetadata method,
            object?[] parameters)
        {
            var record = new ApiCallRecord
            {
                ModuleName = module.Name,
                MethodName = method.Name,
                Parameters = parameters
            };

            var stopwatch = Stopwatch.StartNew();

            try
            {
                object? result = null;

                var invokeTask = method.Method.Invoke(module.Instance, parameters);
                if (invokeTask is Task t)
                {
                    await t;
                    var resultProperty = invokeTask.GetType().GetProperty("Result");
                    result = resultProperty?.GetValue(invokeTask);
                }
                else
                {
                    result = invokeTask;
                }

                stopwatch.Stop();
                record.Duration = stopwatch.Elapsed;

                if (result != null)
                {
                    // Extract the response code
                    var responseProperty = result.GetType().GetProperty("Response");
                    if (responseProperty != null)
                    {
                        var response = responseProperty.GetValue(result);
                        if (response != null)
                        {
                            var codeProperty = response.GetType().GetProperty("Code");
                            if (codeProperty != null)
                            {
                                record.StatusCode = (int)(codeProperty.GetValue(response) ?? 0);
                            }

                            // Extract the Message (actual JSON response)
                            var messageProperty = response.GetType().GetProperty("Message");
                            if (messageProperty != null)
                            {
                                var message = messageProperty.GetValue(response) as string;
                                if (!string.IsNullOrEmpty(message))
                                {
                                    // Try to parse and pretty-print the message
                                    try
                                    {
                                        using var doc = JsonDocument.Parse(message);
                                        record.ResponseJson = JsonSerializer.Serialize(doc, _jsonOptions);
                                    }
                                    catch
                                    {
                                        record.ResponseJson = message;
                                    }
                                }
                            }
                        }
                    }

                    // If no message extracted, try Result property
                    if (string.IsNullOrEmpty(record.ResponseJson))
                    {
                        var resultProp = result.GetType().GetProperty("Result");
                        if (resultProp != null)
                        {
                            var resultValue = resultProp.GetValue(result);
                            if (resultValue != null)
                            {
                                record.ResponseJson = JsonSerializer.Serialize(resultValue, _jsonOptions);
                            }
                        }
                    }

                    // Fallback to full result
                    if (string.IsNullOrEmpty(record.ResponseJson))
                    {
                        record.ResponseJson = JsonSerializer.Serialize(result, _jsonOptions);
                    }
                }
                else
                {
                    record.StatusCode = 200;
                    record.ResponseJson = "null";
                }
            }
            catch (TargetInvocationException ex)
            {
                stopwatch.Stop();
                record.Duration = stopwatch.Elapsed;
                record.StatusCode = 500;
                record.ResponseJson = JsonSerializer.Serialize(new
                {
                    error = ex.InnerException?.Message ?? ex.Message
                }, _jsonOptions);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                record.Duration = stopwatch.Elapsed;
                record.StatusCode = 500;
                record.ResponseJson = JsonSerializer.Serialize(new
                {
                    error = ex.Message
                }, _jsonOptions);
            }

            _historyService.AddRecord(record);
            return record;
        }
    }
}
