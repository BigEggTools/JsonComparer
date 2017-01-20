namespace BigEgg.Tools.JsonComparer.Progress
{
    /// <summary>
    /// The progress report model
    /// </summary>
    public interface IProgressReport
    {
        /// <summary>
        /// Gets the current progress.
        /// </summary>
        /// <value>
        /// The current progress.
        /// </value>
        int Current { get; }

        /// <summary>
        /// Gets the total amount.
        /// </summary>
        /// <value>
        /// The total amount.
        /// </value>
        int Total { get; }

        /// <summary>
        /// Gets the complete percentage of the progress.
        /// </summary>
        /// <value>
        /// The complete percentage of the progress.
        /// </value>
        int Percentage { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; }
    }
}
