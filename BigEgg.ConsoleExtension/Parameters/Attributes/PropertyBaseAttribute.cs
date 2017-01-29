namespace BigEgg.ConsoleExtension.Parameters
{
    using System;

    /// <summary>
    /// The abstract base attribute for parameter model's property
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public abstract class PropertyBaseAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBaseAttribute" /> class.
        /// </summary>
        /// <param name="longName">The name of the command.</param>
        /// <param name="shortName">The name of the command.</param>
        /// <param name="helpMessage">The help message of the command.</param>
        /// <exception cref="ArgumentException">
        /// Throw if <paramref name="longName"/> is null, empty or whitespace.
        /// or
        /// Throw if <paramref name="shortName"/> is null, empty or whitespace.
        /// or
        /// Throw if <paramref name="shortName"/>'s length larger than 4 characters.
        /// or
        /// Throw if <paramref name="helpMessage"/> is null, empty or whitespace
        /// </exception>
        public PropertyBaseAttribute(string longName, string shortName, string helpMessage)
        {
            if (string.IsNullOrWhiteSpace(longName)) { throw new ArgumentException("longName"); }
            if (string.IsNullOrWhiteSpace(shortName)) { throw new ArgumentException("shortName"); }
            if (shortName.Length > 4) { throw new ArgumentException("shortName"); }
            if (string.IsNullOrWhiteSpace(helpMessage)) { throw new ArgumentException("helpMessage"); }

            LongName = longName;
            ShortName = shortName;
            HelpMessage = helpMessage;
        }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>
        /// The name of the command.
        /// </value>
        public string LongName { get; private set; }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>
        /// The name of the command.
        /// </value>
        public string ShortName { get; private set; }

        /// <summary>
        /// Gets or sets the help message.
        /// </summary>
        /// <value>
        /// The help message.
        /// </value>
        /// <exception cref="System.ArgumentException">Throw if set help message to null, empty or whitespace</exception>
        public string HelpMessage { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this parameter is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        protected object DefaultValue { get; set; }
    }
}
