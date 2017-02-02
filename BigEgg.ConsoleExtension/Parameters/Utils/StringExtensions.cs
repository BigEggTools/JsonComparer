namespace BigEgg.ConsoleExtension.Parameters.Utils
{
    using System;
    using System.Collections.Generic;

    internal static class StringExtensions
    {
        public static string JoinNewLine(this IEnumerable<string> lines)
        {
            return string.Join(Environment.NewLine, lines);
        }
    }
}
