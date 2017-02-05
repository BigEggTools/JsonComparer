namespace BigEgg.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BigEgg.ConsoleExtension.Parameters.Errors;
    using BigEgg.ConsoleExtension.Parameters.Results;

    internal class TextBuilder
    {
        internal static string Build(ParserResult result, int maximumDisplayWidth = Constants.DEFAULT_MAX_CONSOLE_LENGTH)
        {
            if (result.ResultType == ParserResultType.ParseFailed)
            {
                return BuildHelp(((ParseFailedResult)result).Errors, maximumDisplayWidth);
            }
            return string.Empty;
        }


        private static string BuildHelp(IEnumerable<Error> errors, int maximumDisplayWidth)
        {
            if (errors.Any(e => e.ErrorType == ErrorType.VersionRequest)) { return BuildVersionText(maximumDisplayWidth); }
            if (errors.Any(e => e.ErrorType == ErrorType.HelpRequest)) { return BuildHelpText(maximumDisplayWidth); }
            throw new NotImplementedException();
        }


        private static string BuildVersionText(int maximumDisplayWidth)
        {
            return OutputFormat.VERSION_INFO.Format(
                ProgramInfo.Default.Title,
                ProgramInfo.Default.Version,
                ProgramInfo.Default.Copyright,
                ProgramInfo.Default.Product,
                maximumDisplayWidth
            );
        }

        private static string BuildHeader(int maximumDisplayWidth)
        {
            return OutputFormat.HEADER.Format(
                ProgramInfo.Default.Title,
                ProgramInfo.Default.Version,
                maximumDisplayWidth
            );
        }

        private static string BuildHelpText(int maximumDisplayWidth)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BuildHeader(maximumDisplayWidth));

            return stringBuilder.ToString();
        }
    }
}
