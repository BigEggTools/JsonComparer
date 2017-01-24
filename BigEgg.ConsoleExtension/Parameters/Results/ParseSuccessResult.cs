namespace BigEgg.ConsoleExtension.Parameters.Results
{
    using System;

    /// <summary>
    /// The parse success result, contains an instance with parsed value.
    /// </summary>
    /// <seealso cref="BigEgg.ConsoleExtension.Parameters.Results.ParserResult" />
    internal sealed class ParseSuccessResult : ParserResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseSuccessResult"/> class.
        /// </summary>
        /// <param name="value">The instance with parsed value.</param>
        /// <exception cref="System.ArgumentNullException">The instance cannot be null.</exception>
        public ParseSuccessResult(object value)
            : base(ParserResultType.ParseSuccess)
        {
            if (value == null) { throw new ArgumentNullException("value"); }

            Value = value;
        }


        /// <summary>
        /// Gets the instance with parsed value.
        /// </summary>
        /// <value>
        /// The instance with parsed value.
        /// </value>
        public object Value { get; private set; }
    }
}
