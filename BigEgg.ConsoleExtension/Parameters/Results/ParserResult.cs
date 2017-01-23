namespace BigEgg.ConsoleExtension.Parameters
{
    /// <summary>
    /// The base parser result.
    /// </summary>
    public abstract class ParserResult
    {
        internal ParserResult(ParserResultType resultType)
        {
            ResultType = resultType;
        }

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public ParserResultType ResultType { get; private set; }
    }
}
