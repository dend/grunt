using System;
using System.Collections.Generic;

namespace Den.Dev.Grunt.Zeta.Models
{
    public class ApiCallRecord
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string ModuleName { get; set; } = string.Empty;
        public string MethodName { get; set; } = string.Empty;
        public object?[]? Parameters { get; set; }
        public string? ParametersJson { get; set; }
        public List<ApiParameterInfo> ParameterDetails { get; set; } = new();
        public string? ResponseJson { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;
        public TimeSpan Duration { get; set; }

        // HTTP diagnostic properties
        public string? RequestUrl { get; set; }
        public string? RequestMethod { get; set; }
        public Dictionary<string, string>? RequestHeaders { get; set; }
        public string? RequestBody { get; set; }
        public Dictionary<string, string>? ResponseHeaders { get; set; }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {ModuleName}.{MethodName} - {(IsSuccess ? "OK" : $"Error {StatusCode}")}";
        }
    }

    public class ApiParameterInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Value { get; set; }
    }
}
