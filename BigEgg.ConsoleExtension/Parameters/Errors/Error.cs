namespace BigEgg.ConsoleExtension.Parameters.Errors
{
    /// <summary>
    /// The base class of error
    /// </summary>
    internal abstract class Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="stopProcessing">if set to <c>true</c> should stops processing parse.</param>
        protected Error(ErrorType errorType, bool stopProcessing)
        {
            ErrorType = errorType;
            StopProcessing = stopProcessing;
        }


        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        public ErrorType ErrorType { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this error should stops processing parse.
        /// </summary>
        /// <value>
        ///   <c>true</c> if should stops processing parse; otherwise, <c>false</c>.
        /// </value>
        public bool StopProcessing { get; set; }
    }
}
