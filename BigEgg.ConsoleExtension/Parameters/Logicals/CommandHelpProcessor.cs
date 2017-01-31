namespace BigEgg.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.ConsoleExtension.Parameters.Results;
    using BigEgg.ConsoleExtension.Parameters.Tokens;
    using BigEgg.ConsoleExtension.Parameters.Errors;
    using BigEgg.ConsoleExtension.Parameters.Utils;

    internal class CommandHelpProcessor : IProcessor
    {
        public bool NeedType { get { return true; } }

        public ParserResult Process(IList<Token> tokens, Type type, bool caseSensitive)
        {
            if (tokens == null) { throw new ArgumentNullException("tokens"); }
            if (type == null) { throw new ArgumentNullException("type"); }

            if (tokens.Any(t => t.TokenType == TokenType.Help)
                && tokens.Any(t => t.TokenType == TokenType.Command))
            {
                var commandToken = tokens.First(t => t.TokenType == TokenType.Command);
                var existCommand = type.GetCommand();
                if (existCommand.Name.Equals(
                        commandToken.Value,
                        caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
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

            return null;
        }
    }
}
