using System;

namespace Den.Dev.Grunt.Zeta.Models
{
    public class ApiCallRecord
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string ModuleName { get; set; } = string.Empty;
        public string MethodName { get; set; } = string.Empty;
        public object?[]? Parameters { get; set; }
        public string? ResponseJson { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;
        public TimeSpan Duration { get; set; }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {ModuleName}.{MethodName} - {(IsSuccess ? "OK" : $"Error {StatusCode}")}";
        }
    }
}
