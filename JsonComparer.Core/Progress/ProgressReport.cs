namespace BigEgg.Tools.JsonComparer.Progress
{
    using System;

    /// <summary>
    /// The progress report model
    /// </summary>
    public class ProgressReport
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressReport"/> class.
        /// </summary>
        /// <param name="current">The current progress.</param>
        /// <param name="total">The total amount.</param>
        public ProgressReport(int current, int total)
            : this(current, total, string.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressReport" /> class.
        /// </summary>
        /// <param name="current">The current progress.</param>
        /// <param name="total">The total amount.</param>
        /// <param name="message">The message.</param>
        public ProgressReport(int current, int total, string message)
        {
            if (current < 0) { throw new ArgumentException("current"); }
            if (total < 0) { throw new ArgumentException("total"); }
            if (total < current) { throw new ArgumentException("current cannot larger than total"); }
            if (message == null) { throw new ArgumentNullException("message"); }

            Current = current;
            Total = total;
            Message = message;
        }


        /// <summary>
        /// Gets the current progress.
        /// </summary>
        /// <value>
        /// The current progress.
        /// </value>
        public int Current { get; private set; }

        /// <summary>
        /// Gets the total amount.
        /// </summary>
        /// <value>
        /// The total amount.
        /// </value>
        public int Total { get; private set; }

        /// <summary>
        /// Gets the complete percentage of the progress.
        /// </summary>
        /// <value>
        /// The complete percentage of the progress.
        /// </value>
        public int Percentage { get { return (int)((Current * 1.0 / Total) * 100); } }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }
    }
}
