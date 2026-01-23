using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI
{
    public static class Theme
    {
        // Spinner - circle pattern
        public static readonly Spinner Spinner = Spinner.Known.Circle;

        // Selection character
        public const string SelectionCursor = "❯";

        // Colors
        public static readonly Color Accent = Color.Cyan1;
        public static readonly Color Muted = Color.Grey;
        public static readonly Color Success = Color.Green;
        public static readonly Color Error = Color.Red;
        public static readonly Color Warning = Color.Yellow;

        // Box drawing
        public const char BoxHorizontal = '─';
        public const char BoxVertical = '│';
        public const char BoxTopLeft = '╭';
        public const char BoxTopRight = '╮';
        public const char BoxBottomLeft = '╰';
        public const char BoxBottomRight = '╯';

        // Selection prompt styling
        public static SelectionPrompt<T> ApplyStyle<T>(this SelectionPrompt<T> prompt) where T : notnull
        {
            return prompt
                .HighlightStyle(new Style(Accent))
                .MoreChoicesText($"[{Muted.ToMarkup()}](Move up/down to see more)[/]");
        }
    }
}
