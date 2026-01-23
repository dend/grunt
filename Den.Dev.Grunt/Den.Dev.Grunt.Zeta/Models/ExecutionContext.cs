using Den.Dev.Grunt.Core;
using Den.Dev.Grunt.Models.Security;

namespace Den.Dev.Grunt.Zeta.Models
{
    public class ExecutionContext
    {
        public OAuthToken? OAuthToken { get; set; }
        public string SpartanToken { get; set; } = string.Empty;
        public string Xuid { get; set; } = string.Empty;
        public string? Gamertag { get; set; }
        public string ClearanceToken { get; set; } = string.Empty;
        public HaloInfiniteClient? HaloClient { get; set; }
        public WaypointClient? WaypointClient { get; set; }
        public bool IsAuthenticated => HaloClient != null;
        public bool VerboseDiagnosticsEnabled { get; set; } = false;
    }
}
