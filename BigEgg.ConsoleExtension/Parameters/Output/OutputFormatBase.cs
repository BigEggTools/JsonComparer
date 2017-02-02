namespace BigEgg.ConsoleExtension.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.ConsoleExtension.Parameters.Utils;

    internal abstract class OutputFormatBase
    {
        protected string INDEX_START_STRING = "|:";

        protected string Format(string[] formatString, IList<Tuple<string, string>> args, int maximumDisplayWidth)
        {
            return formatString.Select(line =>
            {
                foreach (var arg in args)
                {
                    if (line.Contains(arg.Item1))
                    {
                        return ConcatWithWidth(line.Replace(arg.Item1, ""), arg.Item2, maximumDisplayWidth);
                    }
                }
                return ConcatWithWidth(line, string.Empty, maximumDisplayWidth);
            }).JoinNewLine();
        }


        private string ConcatWithWidth(string format, string value, int maximumDisplayWidth)
        {
            var index = format.IndexOf(INDEX_START_STRING);
            var result = format.Replace(INDEX_START_STRING, "");

            var lines = new List<string>();
            if (index == -1)
            {
                result += value;
                for (int i = 0; i <= result.Length / maximumDisplayWidth; i++)
                {
                    lines.Add(
                        result.Substring(
                            i * maximumDisplayWidth,
                            Math.Min(result.Length - i * maximumDisplayWidth, maximumDisplayWidth)));
                }
            }
            else
            {
                var valueLenght = (maximumDisplayWidth - index);
                for (int i = 0; i <= value.Length / valueLenght; i++)
                {
                    if (i == 0)
                    {
                        lines.Add(
                            result + value.Substring(
                                i * valueLenght,
                                Math.Min(value.Length, valueLenght)));
                    }
                    else
                    {
                        lines.Add(
                            new string(' ', index) + value.Substring(
                                i * valueLenght,
                                Math.Min(value.Length - i * valueLenght, valueLenght)));
                    }
                }
            }

            return lines.JoinNewLine();
        }
    }
}
