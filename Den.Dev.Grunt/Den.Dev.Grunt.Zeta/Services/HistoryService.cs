using System.Collections.Generic;
using System.Linq;
using Den.Dev.Grunt.Zeta.Models;

namespace Den.Dev.Grunt.Zeta.Services
{
    public class HistoryService
    {
        private readonly List<ApiCallRecord> _history = new();
        private const int MaxHistorySize = 100;

        public IReadOnlyList<ApiCallRecord> History => _history.AsReadOnly();

        public void AddRecord(ApiCallRecord record)
        {
            _history.Insert(0, record);

            if (_history.Count > MaxHistorySize)
            {
                _history.RemoveAt(_history.Count - 1);
            }
        }

        public void Clear()
        {
            _history.Clear();
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
