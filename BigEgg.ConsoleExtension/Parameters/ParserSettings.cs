namespace BigEgg.ConsoleExtension.Parameters
{
    using System;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// The setting model for <see cref="BigEgg.ConsoleExtension.Parameters.Parser"/>.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class ParserSettings : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserSettings"/> class.
        /// </summary>
        private ParserSettings() { }

        /// <summary>
        /// Finalizes an instance of the <see cref="ParserSettings"/> class.
        /// </summary>
        ~ParserSettings()
        {
            Dispose(false);
        }


        /// <summary>
        /// Gets the <see cref="System.IO.TextWriter"/> used for help method output.
        /// </summary>
        /// <value>
        /// The <see cref="System.IO.TextWriter"/> used for help method output.
        /// </value>
        /// <remarks>
        /// Default is the <see cref="System.Console.Error"/> text writer.
        /// It is the caller's responsibility to dispose or close the <see cref="TextWriter"/>.
        /// </remarks>
        public TextWriter HelpWriter { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the parser shall ignore the given argument if it is an unknown arguments.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the parser ignore the given argument when it is an unknown arguments; otherwise, <c>false</c>.
        /// </value>
        public bool CaseSensitive { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the parser shall ignore the given argument if it encounter an unknown arguments
        /// </summary>
        /// <value>
        /// <c>true</c> if the parser shall ignore the given argument if it encounter an unknown arguments; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreUnknownArguments { get; private set; }

        /// <summary>
        /// Gets the maximum width of the display.
        /// </summary>
        /// <value>
        /// The maximum width of the display.
        /// </value>
        public int MaximumDisplayWidth { get; private set; }


        /// <summary>
        /// The builder to create a parser settings model
        /// </summary>
        /// <returns>The builder to create a parser settings model</returns>
        public static ParserSettingsBuilder Builder()
        {
            return new ParserSettingsBuilder();
        }


        /// <summary>
        /// Releases managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) { return; }

            if (disposing)
            {
                // Do not dispose HelpWriter. It is the caller's responsibility.
                disposed = true;
            }
        }


        /// <summary>
        /// The builder to create a parser settings model
        /// </summary>
        public class ParserSettingsBuilder
        {
            private ParserSettings instance;

            /// <summary>
            /// Initializes a new instance of the <see cref="ParserSettingsBuilder"/> class.
            /// </summary>
            public ParserSettingsBuilder()
            {
                instance = new ParserSettings();
            }


            /// <summary>
            /// Use the default settings.
            /// </summary>
            /// <returns>The <see cref="ParserSettingsBuilder"/>.</returns>
            public ParserSettingsBuilder WithDefault()
            {
                instance.HelpWriter = Console.Error;
                instance.CaseSensitive = true;
                instance.IgnoreUnknownArguments = true;
                instance.MaximumDisplayWidth = Constants.DEFAULT_MAX_CONSOLE_LENGTH;
                return this;
            }

            /// <summary>
            /// Sets the <see cref="System.IO.TextWriter"/> used for help method output.
            /// </summary>
            /// <returns>The <see cref="ParserSettingsBuilder"/>.</returns>
            public ParserSettingsBuilder HelpWriter(TextWriter helpWriter)
            {
                if (helpWriter == null) { throw new ArgumentNullException("helpWriter"); }
                instance.HelpWriter = helpWriter;
                return this;
            }

            /// <summary>
            /// Sets a value indicating whether the parser shall ignore the given argument if it is an unknown arguments.
            /// </summary>
            /// <returns>The <see cref="ParserSettingsBuilder"/>.</returns>
            public ParserSettingsBuilder CaseSensitive(bool caseSensitive)
            {
                instance.CaseSensitive = caseSensitive;
                return this;
            }

            /// <summary>
            /// Sets a value indicating whether the parser shall ignore the given argument if it encounter an unknown arguments
            /// </summary>
            /// <returns>The <see cref="ParserSettingsBuilder"/>.</returns>
            public ParserSettingsBuilder IgnoreUnknownArguments(bool ignoreUnknownArguments)
            {
                instance.IgnoreUnknownArguments = ignoreUnknownArguments;
                return this;
            }

            /// <summary>
            /// Sets the maximum width of the display.
            /// </summary>
            /// <returns>The <see cref="ParserSettingsBuilder"/>.</returns>
            public ParserSettingsBuilder ComputeDisplayWidth()
            {
                try
                {
                    instance.MaximumDisplayWidth = Console.WindowWidth;
                }
                catch (IOException)
                {
                    instance.MaximumDisplayWidth = Constants.DEFAULT_MAX_CONSOLE_LENGTH;
                }
                return this;
            }

            /// <summary>
            /// Return the instance of parser settings model.
            /// </summary>
            /// <returns>The instance of parser settings model</returns>
            /// <exception cref="System.ArgumentNullException">
            /// HelpWriter cannot be null.
            /// or
            /// ParsingCulture cannot be null.
            /// </exception>
            public ParserSettings Build()
            {
                if (instance.HelpWriter == null) { throw new ArgumentNullException("HelpWriter"); }
                return instance;
            }
        }
    }
}
