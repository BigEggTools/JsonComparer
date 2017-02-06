namespace BigEgg.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BigEgg.ConsoleExtension.Parameters.Utils;

    internal abstract class OutputFormatBase
    {
        protected string INDEX_START_STRING = "|:";

        protected string Format(string[] formatString, IList<Tuple<string, string>> args, int maximumDisplayWidth)
        {
            return formatString.Select(line =>
            {
                var replaceItem = args.Where(arg => line.Contains(arg.Item1)).ToList();
                return ConcatWithWidth(line, replaceItem, maximumDisplayWidth);
            }).Join();
        }


        private string ConcatWithWidth(string format, List<Tuple<string, string>> args, int maximumDisplayWidth)
        {
            var result = format;

            args.ForEach(arg => result = result.Replace(arg.Item1, arg.Item2));
            var index = result.IndexOf(INDEX_START_STRING);
            result = result.Replace(INDEX_START_STRING, "");

            var stringBuilder = new StringBuilder();
            if (index == -1)
            {
                for (int i = 0; i <= result.Length / maximumDisplayWidth; i++)
                {
                    stringBuilder.AppendLine(
                        result.Substring(
                            i * maximumDisplayWidth,
                            Math.Min(result.Length - i * maximumDisplayWidth, maximumDisplayWidth)));
                }
            }
            else
            {
                var valueLenght = (maximumDisplayWidth - index);
                if (valueLenght <= 0) { throw new FormatException(); }

                for (int i = 0; i <= (result.Length - index) / valueLenght; i++)
                {
                    if (i == 0)
                    {
                        stringBuilder.AppendLine(
                            result.Substring(0, Math.Min(result.Length, valueLenght)));
                    }
                    else
                    {
                        stringBuilder.Append(' ', index);
                        stringBuilder.AppendLine(
                             result.Substring(
                                index + i * valueLenght,
                                Math.Min(result.Length - index - i * valueLenght, valueLenght)));
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
