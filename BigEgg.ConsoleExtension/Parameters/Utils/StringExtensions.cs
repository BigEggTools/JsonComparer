namespace BigEgg.ConsoleExtension.Parameters.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal static class StringExtensions
    {
        public static string JoinNewLine(this IEnumerable<StringBuilder> builders)
        {
            return string.Join(Environment.NewLine, builders);
        }
    }
}
