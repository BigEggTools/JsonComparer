namespace BigEgg.ConsoleExtension.Parameters.Tokens
{
    internal class PropertyToken : Token
    {
        public PropertyToken(string name, string value)
            : base(name, TokenType.Property, value)
        { }
    }
}
