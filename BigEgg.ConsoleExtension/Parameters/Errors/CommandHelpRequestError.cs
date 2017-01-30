namespace BigEgg.ConsoleExtension.Parameters.Errors
{
    using System;

    internal class CommandHelpRequestError : Error
    {
        public CommandHelpRequestError(string commandName, bool existed, Type commandType)
            : base(ErrorType.CommandHelpRequest, true)
        {
            CommandName = commandName;
            Existed = existed;
            CommandType = existed ? commandType : null;
        }


        public string CommandName { get; private set; }

        public bool Existed { get; private set; }

        public Type CommandType { get; private set; }
    }
}
