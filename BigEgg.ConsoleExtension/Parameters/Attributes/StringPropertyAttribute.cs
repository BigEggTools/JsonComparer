namespace BigEgg.ConsoleExtension.Parameters
{
    using System;

    /// <summary>
    /// The attribute of adding the metadata of the parameter model's property
    /// </summary>
    /// <seealso cref="BigEgg.ConsoleExtension.Parameters.PropertyBaseAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class StringPropertyAttribute : PropertyBaseAttribute
    {
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
        public StringPropertyAttribute(string longName, string shortName, string helpMessage)
            : base(longName, shortName, helpMessage)
        { }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public new string DefaultValue
        {
            get { return base.DefaultValue as string; }
            set { base.DefaultValue = value; }
        }
    }
}
