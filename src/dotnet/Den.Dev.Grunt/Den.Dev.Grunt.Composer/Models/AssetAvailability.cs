using SQLite;

namespace Den.Dev.Grunt.Composer.Models
{
    internal class AssetAvailability
    {
        [Column("MAP_AVAILABLE")]
        public bool MapAvailable { get; set; }

        [Column("PLAYLIST_AVAILABLE")]
        public bool PlaylistAvailable { get; set; }

        [Column("PLAYLISTMAPMODEPAIR_AVAILABLE")]
        public bool PlaylistMapModePairAvailable { get; set; }

        [Column("GAMEVARIANT_AVAILABLE")]
        public bool GameVariantAvailable { get; set; }

        [Column("ENGINEGAMEVARIANT_AVAILABLE")]
        public bool EngineGameVariantAvailable { get; set; }
    }
}
