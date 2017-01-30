namespace BigEgg.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;

    using BigEgg.ConsoleExtension.Parameters.Results;
    using BigEgg.ConsoleExtension.Parameters.Tokens;

    internal interface IProcessor
    {
        ParserResult Process(IList<Token> tokens, IList<Type> types, bool CaseSensitive);
    }
}
