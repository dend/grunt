using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Den.Dev.Grunt.Zeta.Models;

namespace Den.Dev.Grunt.Zeta.Services
{
    public class HistoryService
    {
        private readonly List<ApiCallRecord> _history = new();
        private const int MaxHistorySize = 100;

        private static readonly string HistoryDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Den.Dev",
            "Grunt.Zeta");

        private static readonly string HistoryFilePath = Path.Combine(HistoryDirectory, "history.json");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        public IReadOnlyList<ApiCallRecord> History => _history.AsReadOnly();

        public void Load()
        {
            try
            {
                if (File.Exists(HistoryFilePath))
                {
                    var json = File.ReadAllText(HistoryFilePath);
                    var records = JsonSerializer.Deserialize<List<ApiCallRecord>>(json, JsonOptions);
                    if (records != null)
                    {
                        _history.Clear();
                        _history.AddRange(records);
                    }
                }
            }
            catch
            {
                // If loading fails, start with empty history
            }
        }

        public void Save()
        {
            try
            {
                if (!Directory.Exists(HistoryDirectory))
                {
                    Directory.CreateDirectory(HistoryDirectory);
                }

                var json = JsonSerializer.Serialize(_history, JsonOptions);
                File.WriteAllText(HistoryFilePath, json);
            }
            catch
            {
                // Silently fail if we can't save
            }
        }

        public void AddRecord(ApiCallRecord record)
        {
            _history.Insert(0, record);

            if (_history.Count > MaxHistorySize)
            {
                _history.RemoveAt(_history.Count - 1);
            }

            Save();
        }

        public void Clear()
        {
            _history.Clear();
            Save();
        }

        public IEnumerable<ApiCallRecord> GetRecentCalls(int count = 10)
        {
            return _history.Take(count);
        }

        public IEnumerable<ApiCallRecord> GetSuccessfulCalls()
        {
            return _history.Where(r => r.IsSuccess);
        }

        public IEnumerable<ApiCallRecord> GetFailedCalls()
        {
            return _history.Where(r => !r.IsSuccess);
        }
    }
}
