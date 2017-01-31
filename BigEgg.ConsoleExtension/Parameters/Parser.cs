namespace BigEgg.ConsoleExtension.Parameters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.ConsoleExtension.Parameters.Output;
    using BigEgg.ConsoleExtension.Parameters.Results;
    using BigEgg.ConsoleExtension.Parameters.Tokens;
    using Logicals;

    /// <summary>
    /// The parser to parse the console arguments
    /// </summary>
    public class Parser : IDisposable
    {
        private static readonly Lazy<Parser> defaultParser = new Lazy<Parser>(() =>
            new Parser()
        );
        private readonly ParserSettings settings;
        private readonly ProcessorEngine engine;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class with a specific setting, <seealso cref="ParserSettings"/>.
        /// </summary>
        public Parser()
            : this(ParserSettings.Builder().WithDefault()
                                        .Build())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class with a specific setting, <seealso cref="ParserSettings"/>.
        /// </summary>
        /// <param name="settings">The parser settings.</param>
        public Parser(ParserSettings settings)
        {
            this.settings = settings;

            engine = new ProcessorEngine(this.settings);
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
            get { return defaultParser.Value; }
        }


        /// <summary>
        /// Parses the console arguments to command.
        /// </summary>
        /// <param name="args">The console arguments.</param>
        /// <param name="types">The supported command types.</param>
        /// <returns>The command type</returns>
        public object Parse(IEnumerable<string> args, params Type[] types)
        {
            if (args == null) throw new ArgumentNullException("args");
            if (types == null) throw new ArgumentNullException("types");
            if (types.Length == 0) throw new ArgumentOutOfRangeException("types");

            var tokens = args.ToList().ToTokens();

            ParserResult result = engine.Handle(tokens, types.ToList());

            settings.HelpWriter.Write(TextBuilder.Build(result, settings.MaximumDisplayWidth));
            return result.ResultType == ParserResultType.ParseSuccess ?
                ((ParseSuccessResult)result).Value :
                null;
        }

        #region Implement IDisposable Interface
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
        #endregion
    }
}
