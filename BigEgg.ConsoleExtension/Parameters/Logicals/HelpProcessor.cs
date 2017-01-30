namespace BigEgg.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.ConsoleExtension.Parameters.Results;
    using BigEgg.ConsoleExtension.Parameters.Tokens;
    using BigEgg.ConsoleExtension.Parameters.Errors;
    using BigEgg.ConsoleExtension.Parameters.Utils;

    internal class HelpProcessor : IProcessor
    {
        public ParserResult Process(IList<Token> tokens, IList<Type> types, bool CaseSensitive)
        {
            if (tokens == null) { throw new ArgumentNullException("tokens"); }
            if (types == null) { throw new ArgumentNullException("types"); }

            if (tokens.Any(t => t.TokenType == TokenType.Help))
            {
                if (tokens.Any(t => t.TokenType == TokenType.Command))
                {
                    var commandToken = tokens.First(t => t.TokenType == TokenType.Command);
                    foreach (var type in types)
                    {
                        var existCommand = type.GetCommand();
                        if (existCommand.Name.Equals(
                                commandToken.Value,
                                CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
                        {
                            return new ParseFailedResult(new List<Error>()
                            {
                                new CommandHelpRequestError(
                                    commandToken.Name,
                                    existCommand != null,
                                    existCommand != null ? type : null
                                )
                            });
                        }
                    }
                    return new ParseFailedResult(new List<Error>()
                    {
                        new CommandHelpRequestError(commandToken.Name, false, null)
                    });
                }
                return new ParseFailedResult(new List<Error>() { new HelpRequestError() });
            }
            return null;
        }
    }
}
