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
        /// <param name="caseSensitive">The value indicating whether the parser shall ignore the given argument if it is an unknown arguments.</param>
        /// <param name="ignoreUnknownArguments">The value indicating whether the parser shall ignore the given argument if it encounter an unknown arguments.</param>
        public ParserSettings(bool caseSensitive = true, bool ignoreUnknownArguments = true)
            : this(Console.Error, CultureInfo.InvariantCulture, caseSensitive, ignoreUnknownArguments)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserSettings"/> class.
        /// </summary>
        /// <param name="helpWriter">The <see cref="System.IO.TextWriter"/> used for help method output.</param>
        /// <param name="caseSensitive">The value indicating whether the parser shall ignore the given argument if it is an unknown arguments.</param>
        /// <param name="ignoreUnknownArguments">The value indicating whether the parser shall ignore the given argument if it encounter an unknown arguments.</param>
        public ParserSettings(TextWriter helpWriter, bool caseSensitive = true, bool ignoreUnknownArguments = true)
            : this(helpWriter, CultureInfo.InvariantCulture, caseSensitive, ignoreUnknownArguments)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserSettings"/> class.
        /// </summary>
        /// <param name="parsingCulture">The culture used when parsing arguments to typed properties.</param>
        /// <param name="caseSensitive">The value indicating whether the parser shall ignore the given argument if it is an unknown arguments.</param>
        /// <param name="ignoreUnknownArguments">The value indicating whether the parser shall ignore the given argument if it encounter an unknown arguments.</param>
        public ParserSettings(CultureInfo parsingCulture, bool caseSensitive = true, bool ignoreUnknownArguments = true)
            : this(Console.Error, parsingCulture, caseSensitive, ignoreUnknownArguments)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserSettings" /> class.
        /// </summary>
        /// <param name="helpWriter">The <see cref="System.IO.TextWriter"/> used for help method output.</param>
        /// <param name="parsingCulture">The culture used when parsing arguments to typed properties.</param>
        /// <param name="caseSensitive">The value indicating whether the parser shall ignore the given argument if it is an unknown arguments.</param>
        /// <param name="ignoreUnknownArguments">The value indicating whether the parser shall ignore the given argument if it encounter an unknown arguments.</param>
        public ParserSettings(TextWriter helpWriter, CultureInfo parsingCulture, bool caseSensitive = true, bool ignoreUnknownArguments = true)
        {
            HelpWriter = helpWriter;
            ParsingCulture = parsingCulture;
            CaseSensitive = caseSensitive;
            IgnoreUnknownArguments = ignoreUnknownArguments;

            try
            {
                MaximumDisplayWidth = Console.WindowWidth;
            }
            catch (IOException)
            {
                MaximumDisplayWidth = Constants.DEFAULT_MAX_CONSOLE_LENGTH;
            }
        }

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
        /// Gets the culture used when parsing arguments to typed properties.
        /// </summary>
        /// <value>
        /// The culture used when parsing arguments to typed properties.
        /// </value>
        /// <remarks>
        /// Default is invariant culture, <see cref="System.Globalization.CultureInfo.InvariantCulture"/>.
        /// </remarks>
        public CultureInfo ParsingCulture { get; private set; }

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
    }
}
