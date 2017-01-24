using System;

namespace BigEgg.ConsoleExtension.Parameters.Errors
{
    internal class CommandHelpRequestError : Error
    {
        public CommandHelpRequestError()
            : base(ErrorType.CommandHelpRequest, true)
        { }


        public string CommandName { get; private set; }

        public bool Existed { get; set; }

        public Type CommandType { get; set; }
    }
}
