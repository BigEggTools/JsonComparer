namespace BigEgg.ConsoleExtension.Parameters.Results
{
    using System;

    /// <summary>
    /// The parse success result, contains an instance with parsed value.
    /// </summary>
    /// <typeparam name="T">The type of the verb class</typeparam>
    /// <seealso cref="BigEgg.ConsoleExtension.Parameters.ParserResult" />
    public sealed class ParseSuccessResult<T> : ParserResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseSuccessResult{T}"/> class.
        /// </summary>
        /// <param name="value">The instance with parsed value.</param>
        /// <exception cref="System.ArgumentNullException">The instance cannot be null.</exception>
        public ParseSuccessResult(T value)
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
        public T Value { get; private set; }
    }
}
