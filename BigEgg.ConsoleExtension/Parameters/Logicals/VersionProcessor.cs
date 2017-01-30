namespace BigEgg.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.ConsoleExtension.Parameters.Results;
    using BigEgg.ConsoleExtension.Parameters.Tokens;
    using BigEgg.ConsoleExtension.Parameters.Errors;

    internal class VersionProcessor : IProcessor
    {
        public ParserResult Process(IList<Token> tokens, IList<Type> types, bool CaseSensitive)
        {
            if (tokens == null) { throw new ArgumentNullException("tokens"); }
            if (types == null) { throw new ArgumentNullException("types"); }

            if (tokens.Any(t => t.TokenType == TokenType.Version))
            {
                return new ParseFailedResult(new List<Error>() { new VersionRequestError() });
            }
            return null;
        }
    }
}
