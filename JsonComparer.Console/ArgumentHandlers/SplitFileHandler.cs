namespace BigEgg.Tools.JsonComparer.ArgumentHandlers
{
    using System;
    using System.ComponentModel.Composition;
    using System.Threading.Tasks;

    using BigEgg.Progress;
    using BigEgg.Tools.ConsoleExtension.ProgressBar;

    using BigEgg.Tools.JsonComparer.Parameters;
    using BigEgg.Tools.JsonComparer.Services.FileActions;

    [Export(typeof(IArgumentHandler))]
    public class SplitFileHandler : IArgumentHandler
    {
        private readonly IFileActionService service;

        [ImportingConstructor]
        public SplitFileHandler(IFileActionService service)
        {
            this.service = service;
        }


        public bool CanHandle(object parameter)
        {
            return parameter is SplitParameter;
        }

        public async Task Handle(object parameterObj)
        {
            var parameter = parameterObj as SplitParameter;
            Console.WriteLine($"Start Split the File: {parameter.FileName}");
            Console.WriteLine($"On Node: {parameter.NodeName}");
            Console.WriteLine($"To Path: {parameter.OutputPath}");

            var progress = new Progress<IProgressReport>(report =>
            {
                TextProgressBar.Draw(report.Current, report.Total);
            });

            if (string.IsNullOrWhiteSpace(parameter.OutputFileNamePattern))
            {
                await service.SplitFile(parameter.FileName, parameter.OutputPath, parameter.NodeName, progress);
            }
            else
            {
                Console.WriteLine($"Output File Name Pattern: {parameter.OutputFileNamePattern}");
                await service.SplitFile(parameter.FileName, parameter.OutputPath, parameter.NodeName, parameter.OutputFileNamePattern, progress);
            }
        }
    }
}
