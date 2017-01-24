namespace BigEgg.ConsoleExtension.Parameters.Output
{
    using System;

    internal static class OutputFormat
    {
        public static string PROGRAM_INFO = "{0} {1}";
        public static string[] VERSION_TITLE = new string[]
        {
            "Program version information:",
            "Program Name: {0}",
            "Program Version: {1}",
            "Program Product: {2}",
            "Program Copyright: {3}",
        };


        public static string FormatWithNewLine(this string format, params string[] args)
        {
            return string.Format(format, args) + Environment.NewLine;
        }

        public static string FormatWithNewLine(this string[] format, params string[] args)
        {
            var allFormat = string.Join(Environment.NewLine, format);
            return FormatWithNewLine(allFormat, args);
        }
    }
}
