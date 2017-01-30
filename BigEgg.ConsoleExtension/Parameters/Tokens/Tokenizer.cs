namespace BigEgg.ConsoleExtension.Parameters.Tokens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Tokenizer
    {
        public static IList<Token> ToTokens(this IList<string> args)
        {
            var result = new List<Token>();
            var index = 0;
            if (!WithPrefixDash(args[0]))
            {
                result.Add(new CommandToken(args.First()));
                index++;
            }

            var tokenName = string.Empty;
            for (; index < args.Count; index++)
            {
                if (WithPrefixDash(args[index]))
                {
                    if (!string.IsNullOrWhiteSpace(tokenName))
                    {
                        ParseFlagToken(result, tokenName);
                        tokenName = string.Empty;
                    }
                    tokenName = GetTokenName(args[index]);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(tokenName))
                    {
                        result.Add(new UnknownToken(args[index]));
                    }
                    else
                    {
                        result.Add(new PropertyToken(tokenName, args[index]));
                        tokenName = string.Empty;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(tokenName))
            {
                ParseFlagToken(result, tokenName);
            }

            return result;
        }

        private static bool WithPrefixDash(string arg)
        {
            return arg.StartsWith(ParameterConstants.PROPERTY_NAME_PREFIX_DASH);
        }

        private static string GetTokenName(string arg)
        {
            return arg.Substring(2);
        }

        private static void ParseFlagToken(IList<Token> tokens, string tokenName)
        {
            if (tokenName.Equals(ParameterConstants.TOKEN_HELP_NAME, StringComparison.OrdinalIgnoreCase))
            {
                tokens.Add(new HelpToken());
            }
            else if (tokenName.Equals(ParameterConstants.TOKEN_VERSION_NAME, StringComparison.OrdinalIgnoreCase))
            {
                tokens.Add(new VersionToken());
            }
            else
            {
                tokens.Add(new FlagToken(tokenName));
            }
        }
    }
}
