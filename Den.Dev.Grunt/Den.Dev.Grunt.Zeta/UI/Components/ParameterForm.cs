using System;
using System.Reflection;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Components
{
    public static class ParameterForm
    {
        public static object?[] CollectParameters(ParameterInfo[] parameters, string xuid)
        {
            var values = new object?[parameters.Length];

            if (parameters.Length == 0)
            {
                return values;
            }

            AnsiConsole.WriteLine();

            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                var paramType = param.ParameterType;
                var isOptional = param.IsOptional;
                var defaultValue = param.DefaultValue;

                values[i] = CollectParameter(param.Name ?? $"param{i}", paramType, isOptional, defaultValue, xuid);
            }

            return values;
        }

        private static object? CollectParameter(string name, Type type, bool isOptional, object? defaultValue, string xuid)
        {
            var displayName = FormatParameterName(name);
            var typeHint = GetTypeHint(type);
            var defaultHint = GetDefaultHint(defaultValue, isOptional);
            var suggestion = GetSuggestion(name, xuid);

            // Handle nullable types
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            // Handle enums
            if (underlyingType.IsEnum)
            {
                return CollectEnumParameter(displayName, typeHint, defaultHint, underlyingType, isOptional, defaultValue);
            }

            // Handle booleans
            if (underlyingType == typeof(bool))
            {
                return CollectBoolParameter(displayName, typeHint, defaultHint, isOptional, defaultValue);
            }

            // Handle numeric types
            if (underlyingType == typeof(int))
            {
                return CollectIntParameter(displayName, typeHint, defaultHint, isOptional, defaultValue);
            }

            // Handle strings (default case)
            return CollectStringParameter(displayName, typeHint, defaultHint, isOptional, defaultValue, suggestion);
        }

        private static object? CollectEnumParameter(string name, string typeHint, string defaultHint, Type enumType, bool isOptional, object? defaultValue)
        {
            var choices = Enum.GetNames(enumType);

            AnsiConsole.MarkupLine($"  [dim]{typeHint}{defaultHint}[/]");

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"  [cyan]{name}[/]")
                    .HighlightStyle(new Style(UI.Theme.Accent))
                    .AddChoices(choices));

            return Enum.Parse(enumType, selection);
        }

        private static bool CollectBoolParameter(string name, string typeHint, string defaultHint, bool isOptional, object? defaultValue)
        {
            AnsiConsole.MarkupLine($"  [dim]{typeHint}{defaultHint}[/]");

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"  [cyan]{name}[/]")
                    .HighlightStyle(new Style(UI.Theme.Accent))
                    .AddChoices("true", "false")) == "true";
        }

        private static int CollectIntParameter(string name, string typeHint, string defaultHint, bool isOptional, object? defaultValue)
        {
            var defaultInt = defaultValue is int i ? i : 0;

            return AnsiConsole.Prompt(
                new TextPrompt<int>($"  [cyan]{name}[/] [dim]({typeHint}{defaultHint})[/]:")
                    .DefaultValue(defaultInt)
                    .ValidationErrorMessage("  [red]Invalid number[/]"));
        }

        private static string? CollectStringParameter(string name, string typeHint, string defaultHint, bool isOptional, object? defaultValue, string? suggestion)
        {
            var defaultStr = defaultValue?.ToString() ?? suggestion ?? string.Empty;

            var promptText = $"  [cyan]{name}[/] [dim]({typeHint}{defaultHint})[/]:";

            var textPrompt = new TextPrompt<string>(promptText)
                .AllowEmpty();

            if (!string.IsNullOrEmpty(defaultStr))
            {
                textPrompt.DefaultValue(defaultStr);
            }

            var result = AnsiConsole.Prompt(textPrompt);

            return string.IsNullOrEmpty(result) && isOptional ? null : result;
        }

        private static string FormatParameterName(string name)
        {
            var result = new System.Text.StringBuilder();
            foreach (char c in name)
            {
                if (char.IsUpper(c) && result.Length > 0)
                {
                    result.Append(' ');
                }
                result.Append(result.Length == 0 ? char.ToUpper(c) : c);
            }
            return result.ToString();
        }

        private static string GetTypeHint(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            if (underlyingType.IsEnum)
            {
                return underlyingType.Name;
            }

            return underlyingType.Name switch
            {
                "String" => "string",
                "Int32" => "int",
                "Int64" => "long",
                "Boolean" => "bool",
                "Guid" => "guid",
                _ => underlyingType.Name.ToLower()
            };
        }

        private static string GetDefaultHint(object? defaultValue, bool isOptional)
        {
            if (defaultValue != null && defaultValue != DBNull.Value)
            {
                return $", default: {defaultValue}";
            }
            if (isOptional)
            {
                return ", optional";
            }
            return string.Empty;
        }

        private static string? GetSuggestion(string paramName, string xuid)
        {
            var lowerName = paramName.ToLower();

            if (lowerName.Contains("player") || lowerName.Contains("xuid"))
            {
                if (!string.IsNullOrEmpty(xuid))
                {
                    return $"xuid({xuid})";
                }
            }

            if (lowerName == "count")
            {
                return "25";
            }

            if (lowerName == "start" || lowerName == "offset")
            {
                return "0";
            }

            if (lowerName == "language" || lowerName == "lang")
            {
                return "en";
            }

            return null;
        }
    }
}
