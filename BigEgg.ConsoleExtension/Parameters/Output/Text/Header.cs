namespace BigEgg.ConsoleExtension.Parameters.Output.Text
{
    using System;
    using System.Collections.Generic;

    internal class Header : OutputFormatBase
    {
        private const string NAME_REPLACER = "|NAME|";
        private const string VERSION_REPLACER = "|VERSION|";
        private string[] formatStrings;

        public Header()
        {
            formatStrings = new string[]
            {
                $"{NAME_REPLACER}: v{VERSION_REPLACER}",
            };
        }

        public string Format(string name, string version, int maximumDisplayWidth)
        {
            return Format(formatStrings, new List<Tuple<string, string>>()
            {
                new Tuple<string, string>(NAME_REPLACER, name),
                new Tuple<string, string>(VERSION_REPLACER, version),
            }, maximumDisplayWidth);
        }

    }
}
