namespace BigEgg.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.ConsoleExtension.Parameters.Errors;

    internal class TextBuilder
    {
        internal static string BuildHelp(IEnumerable<Error> errors, int maximumDisplayWidth = Constants.DEFAULT_MAX_CONSOLE_LENGTH)
        {
            if (errors.Any(e => e.ErrorType == ErrorType.VersionRequest)) { return BuildVersionText(); }
            throw new NotImplementedException();
        }


        private static string BuildVersionText()
        {
            return OutputFormat.VERSION_TITLE.FormatWithNewLine(
                ProgramInfo.Default.Title,
                ProgramInfo.Default.Version,
                ProgramInfo.Default.Copyright,
                ProgramInfo.Default.Product
            );
        }
    }
}
