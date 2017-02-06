namespace BigEgg.ConsoleExtension.Parameters.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class StringExtensions
    {
        public static string Join(this IEnumerable<string> messages)
        {
            return string.Join(
                string.Empty,
                messages.Select(message =>
                    message.EndsWith(Environment.NewLine)
                    ? message
                    : message + Environment.NewLine));
        }
    }
}
