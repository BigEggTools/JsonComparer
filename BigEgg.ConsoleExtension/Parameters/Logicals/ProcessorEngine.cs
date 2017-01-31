namespace BigEgg.ConsoleExtension.Parameters.Logicals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BigEgg.ConsoleExtension.Parameters.Results;
    using BigEgg.ConsoleExtension.Parameters.Tokens;

    internal class ProcessorEngine
    {
        private readonly IList<IProcessor> processors;
        private readonly ParserSettings settings;

        public ProcessorEngine(ParserSettings settings)
        {
            this.settings = settings;

            processors = new List<IProcessor>()
            {
                new VersionProcessor(),
                new CommandHelpProcessor(),
                new DefaultHelpProcessor()
            };
        }


        public ParserResult Handle(IList<Token> tokens, IList<Type> types)
        {
            ParserResult result = null;
            ParserResult newResult = null;
            foreach (var processor in processors)
            {
                if (!processor.NeedType)
                {
                    newResult = processor.Process(tokens, null, settings.CaseSensitive);
                    result = MergeResult(result, newResult);

                    if (!ShouldContinue(result)) { return result; }
                    else { continue; }
                }

                foreach (var type in types)
                {
                    newResult = processor.Process(tokens, type, settings.CaseSensitive);
                    result = MergeResult(result, newResult);

                    if (!ShouldContinue(result)) { return result; }
                }
            }

            return result;
        }

        private ParserResult MergeResult(ParserResult result1, ParserResult result2)
        {
            if (result1 == null || result2.ResultType == ParserResultType.ParseSuccess) { return result2; }
            if (result2 == null || result1.ResultType == ParserResultType.ParseSuccess) { return result1; }

            var failedResult1 = result1 as ParseFailedResult;
            var failedResult2 = result2 as ParseFailedResult;

            return new ParseFailedResult(failedResult1.Errors.Concat(failedResult2.Errors));
        }

        private bool ShouldContinue(ParserResult result)
        {
            if (result == null) { return true; }
            if (result.ResultType == ParserResultType.ParseSuccess) { return false; }

            var failedResult = result as ParseFailedResult;
            return !failedResult.Errors.Any(error => error.StopProcessing);
        }
    }
}
