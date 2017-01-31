namespace BigEgg.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.ConsoleExtension.Parameters.Results;
    using BigEgg.ConsoleExtension.Parameters.Tokens;
    using BigEgg.ConsoleExtension.Parameters.Errors;

    internal class DefaultHelpProcessor : IProcessor
    {
        public bool NeedType { get { return false; } }

        public ParserResult Process(IList<Token> tokens, Type type, bool caseSensitive)
        {
            if (tokens == null) { throw new ArgumentNullException("tokens"); }

            return tokens.Any(t => t.TokenType == TokenType.Help)
                ? new ParseFailedResult(new List<Error>() { new HelpRequestError() })
                : null;
        }
    }
}
