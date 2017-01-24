namespace BigEgg.ConsoleExtension.Parameters.Results
{
    /// <summary>
    /// The base parser result.
    /// </summary>
    internal abstract class ParserResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserResult"/> class.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        protected ParserResult(ParserResultType resultType)
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
