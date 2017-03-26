namespace BigEgg.Tools.JsonComparer.ArgumentHandlers
{
    using System;
    using System.ComponentModel.Composition;
    using System.Threading.Tasks;

    using BigEgg.Progress;
    using BigEgg.Tools.ConsoleExtension.ProgressBar;

    using BigEgg.Tools.JsonComparer.Parameters;
    using BigEgg.Tools.JsonComparer.Services.Compares;

    [Export(typeof(IArgumentHandler))]
    public class CompareHandler : IArgumentHandler
    {
        private readonly ICompareService service;


        [ImportingConstructor]
        public CompareHandler(ICompareService compareService)
        {
            service = compareService;
        }


        public bool CanHandle(object parameter)
        {
            return parameter is CompareParameter;
        }

        public async Task Handle(object parameterObj)
        {
            var parameter = parameterObj as CompareParameter;
            Console.WriteLine("Start Compare the Files:");
            Console.WriteLine($"{parameter.Path1}");
            Console.WriteLine("And:");
            Console.WriteLine($"{parameter.Path2}");
            Console.WriteLine($"Config File: {parameter.ConfigFile}");
            Console.WriteLine($"Result will Save to: {parameter.OutputPath}");

            var progress = new Progress<IProgressReport>(report =>
            {
                TextProgressBar.Draw(report.Current, report.Total);
            });
        }
    }
}
