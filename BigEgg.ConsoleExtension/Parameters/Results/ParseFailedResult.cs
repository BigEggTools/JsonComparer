namespace BigEgg.ConsoleExtension.Parameters.Results
{
    using System.Collections.Generic;

    using BigEgg.ConsoleExtension.Parameters.Errors;

    /// <summary>
    /// The parse failed result, contains parsing errors.
    /// </summary>
    internal class ParseFailedResult : ParserResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseFailedResult"/> class.
        /// </summary>
        /// <param name="errors">The parsing errors.</param>
        public ParseFailedResult(IEnumerable<Error> errors)
            : base(ParserResultType.ParseFailed)
        {
            Errors = errors;
        }

        /// <summary>
        /// Gets or sets the parsing errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IEnumerable<Error> Errors { get; private set; }
    }
}
