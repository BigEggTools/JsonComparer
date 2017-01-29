namespace BigEgg.ConsoleExtension.Parameters.Tokens
{
    internal class UnknownToken : Token
    {
        public UnknownToken(string name)
            : base(name, TokenType.Unknown)
        { }
    }
}
