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
    public class MergeFileHandler : IArgumentHandler
    {
        private readonly IFileActionService service;


        [ImportingConstructor]
        public MergeFileHandler(IFileActionService service)
        {
            this.service = service;
        }


        public bool CanHandle(object parameter)
        {
            return parameter is MergeParameter;
        }

        public async Task Handle(object parameterObj)
        {
            var parameter = parameterObj as MergeParameter;
            Console.WriteLine($"Start Merge the Files from: {parameter.FilesPath}");
            Console.WriteLine($"To File: {parameter.FileName}");
            Console.WriteLine($"On Node: {parameter.NodeName}");

            var progress = new Progress<IProgressReport>(report =>
            {
                TextProgressBar.Draw(report.Current, report.Total);
            });

            await service.MergeFiles(parameter.FileName, parameter.NodeName, parameter.FilesPath, progress);
        }
    }
}
