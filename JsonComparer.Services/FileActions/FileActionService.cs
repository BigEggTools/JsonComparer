namespace BigEgg.Tools.JsonComparer.Services.FileActions
{
    using System;
    using System.ComponentModel.Composition;

    using BigEgg.Progress;

    using BigEgg.Tools.JsonComparer.Services.Json;

    /// <summary>
    /// The logic for split JSON file or merge JSON files.
    /// </summary>
    [Export(typeof(IFileActionService))]
    public partial class FileActionService : IFileActionService
    {
        private readonly IJsonDocumentService jsonDocumentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileActionService"/> class.
        /// </summary>
        /// <param name="jsonDocumentService">The JSON document service.</param>
        [ImportingConstructor]
        public FileActionService(IJsonDocumentService jsonDocumentService)
        {
            this.jsonDocumentService = jsonDocumentService;
        }

        private void reportProgress(IProgress<IProgressReport> progress, IProgressReport report)
        {
            if (progress != null)
            {
                progress.Report(report);
            }
        }
    }
}
