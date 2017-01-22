namespace BigEgg.ConsoleExtension.Parameters
{
    using System;

    /// <summary>
    /// The parser to parse the console arguments
    /// </summary>
    public class Parser : IDisposable
    {
        private static readonly Lazy<Parser> DefaultParser = new Lazy<Parser>(
            () => new Parser(ParserSettings.Builder().WithDefault().Build()));
        private readonly ParserSettings settings;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class with a specific setting, <seealso cref="ParserSettings"/>.
        /// </summary>
        /// <param name="settings">The parser settings.</param>
        public Parser(ParserSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Parser"/> class.
        /// </summary>
        ~Parser()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the singleton instance created with default settings.
        /// </summary>
        public static Parser Default
        {
            get { return DefaultParser.Value; }
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
            if (disposed) return;

            if (disposing)
            {
                if (settings != null)
                    settings.Dispose();

                disposed = true;
            }
        }
    }
}
