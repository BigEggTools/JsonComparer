namespace BigEgg.ConsoleExtension.Parameters.Output.Text
{
    using System;
    using System.Collections.Generic;

    internal class VersionInfo : OutputFormatBase
    {
        private const string NAME_REPLACER = "|NAME|";
        private const string VERSION_REPLACER = "|VERSION|";
        private const string PRODUCT_REPLACER = "|PRODUCT|";
        private const string COPYRIGHT_REPLACER = "|COPYRIGHT|";
        private string[] formatStrings;

        public VersionInfo()
        {
            formatStrings = new string[]
            {
                    "Program version information:",
                    $"Program Name: {INDEX_START_STRING}{NAME_REPLACER}",
                    $"Program Version: {INDEX_START_STRING}{VERSION_REPLACER}",
                    $"Program Product: {INDEX_START_STRING}{PRODUCT_REPLACER}",
                    $"Program Copyright: {INDEX_START_STRING}{COPYRIGHT_REPLACER}",
            };
        }

        public string Format(string name, string version, string product, string copyright, int maximumDisplayWidth)
        {
            return Format(formatStrings, new List<Tuple<string, string>>()
            {
                new Tuple<string, string>(NAME_REPLACER, name),
                new Tuple<string, string>(VERSION_REPLACER, version),
                new Tuple<string, string>(PRODUCT_REPLACER, product),
                new Tuple<string, string>(COPYRIGHT_REPLACER, copyright)
            }, maximumDisplayWidth);
        }
    }
}
