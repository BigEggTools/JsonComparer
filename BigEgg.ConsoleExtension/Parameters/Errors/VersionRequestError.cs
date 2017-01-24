namespace BigEgg.ConsoleExtension.Parameters.Errors
{
    internal class VersionRequestError : Error
    {
        public VersionRequestError()
            : base(ErrorType.VersionRequest, true)
        { }
    }
}
