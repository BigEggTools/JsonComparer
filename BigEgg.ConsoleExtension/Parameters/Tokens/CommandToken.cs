namespace BigEgg.ConsoleExtension.Parameters.Tokens
{
    internal class CommandToken : Token
    {
        public CommandToken(string commandName)
            : base(ParameterConstants.TOKEN_COMMAMD_NAME, TokenType.Command, commandName)
        { }
    }
}
