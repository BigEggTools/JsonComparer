namespace BigEgg.ConsoleExtension.Parameters.Errors
{
    internal class HelpRequestError : Error
    {
        public HelpRequestError()
            : base(ErrorType.HelpRequest, true)
        { }
    }
}
