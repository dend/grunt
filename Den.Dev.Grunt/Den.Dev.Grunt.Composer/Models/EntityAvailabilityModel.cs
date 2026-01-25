using SQLite;

namespace Den.Dev.Grunt.Composer.Models
{
    internal class EntityAvailabilityModel
    {
        [Column("MATCH_AVAILABLE")]
        public bool MatchAvailable { get; set; }

        [Column("PLAYER_STATS_AVAILABLE")]
        public bool PlayerStatsAvailable { get; set; }
    }
}
