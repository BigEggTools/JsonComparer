namespace BigEgg.ConsoleExtension.Parameters.Tokens
{
    using System;

    internal abstract class Token
    {
        public Token(string name, TokenType tokenType)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("name"); }

            Name = name;
            TokenType = tokenType;
            Value = string.Empty;
        }

        public Token(string name, TokenType tokenType, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("name"); }
            if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("value"); }

            Name = name;
            TokenType = tokenType;
            Value = value;
        }

        public string Name { get; private set; }

        public TokenType TokenType { get; private set; }

        public string Value { get; private set; }
    }
}
