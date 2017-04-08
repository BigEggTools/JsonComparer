namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BigEgg.Progress;
    using Newtonsoft.Json.Linq;

    using BigEgg.Tools.JsonComparer.CompareData;
    using BigEgg.Tools.JsonComparer.JsonDocuments;
    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;

    /// <summary>
    /// The logic for compare JSON files in 2 folder.
    /// </summary>
    [Export(typeof(ICompareService))]
    internal class CompareService : ICompareService
    {
        private readonly ICompareConfigDocumentType configDocumentType;
        private readonly IReadFileService readFileService;
        private readonly IAnalyzeService analyzeService;

        private readonly IDictionary<FieldType, JTokenType> supportJTokenTypes;


        /// <summary>
        /// Initializes a new instance of the <see cref="CompareService" /> class.
        /// </summary>
        /// <param name="configDocumentType">Type of the configuration document.</param>
        /// <param name="readFileService">The read JSON file service.</param>
        /// <param name="analyzeService">The analyze service.</param>
        [ImportingConstructor]
        public CompareService(ICompareConfigDocumentType configDocumentType, IReadFileService readFileService, IAnalyzeService analyzeService)
        {
            this.configDocumentType = configDocumentType;
            this.readFileService = readFileService;
            this.analyzeService = analyzeService;

            supportJTokenTypes = new Dictionary<FieldType, JTokenType>();
            supportJTokenTypes.Add(FieldType.Boolean, JTokenType.Boolean);
            supportJTokenTypes.Add(FieldType.Integer, JTokenType.Integer);
            supportJTokenTypes.Add(FieldType.String, JTokenType.String);
        }


        /// <summary>
        /// Compares JSON files in 2 folder and output the result.
        /// </summary>
        /// <param name="path1">The directory path of JSON files.</param>
        /// <param name="path2">The another directory path of JSON files.</param>
        /// <param name="configFile">The configuration file.</param>
        /// <param name="progress">The progress update provider.</param>
        /// <returns>All compare result</returns>
        public async Task<IList<CompareFile>> Compare(string path1, string path2, string configFile, IProgress<IProgressReport> progress = null)
        {
            Preconditions.NotNullOrWhiteSpace(path1, "path1");
            Preconditions.NotNullOrWhiteSpace(path2, "path2");
            Preconditions.NotNullOrWhiteSpace(configFile, "configFile");
            Preconditions.Check(!path1.Equals(path2, StringComparison.OrdinalIgnoreCase), "Path1 should not be same as Path2");

            Trace.Indent();
            Trace.TraceInformation($"Start Read the Compare Config File: {configFile}.");
            var config = configDocumentType.Read(configFile);

            Trace.TraceInformation($"Start Compare the folder {path1} with folder {path2}.");
            var filesFromPath1 = Directory.EnumerateFiles(path1).Where(path => Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase)).ToList();
            Trace.TraceInformation($"Have {filesFromPath1.Count} files from {path1}.");
            var filesFromPath2 = Directory.EnumerateFiles(path2).Where(path => Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase)).ToList();
            Trace.TraceInformation($"Have {filesFromPath2.Count} files from {path2}.");

            var fileNames = filesFromPath1.Select(path => Path.GetFileName(path))
                                          .Concat(filesFromPath2.Select(path => Path.GetFileName(path)))
                                          .Distinct().ToList(); ;
            Console.WriteLine($"Start compare the files. Total Number: {fileNames.Count}.");
            var result = new List<CompareFile>(fileNames.Count);
            reportProgress(progress, new ProgressReport(0, fileNames.Count));
            foreach (var fileName in fileNames)
            {
                JsonDocument file1, file2;

                try
                {
                    file1 = await readFileService.GetJsonDocument(Path.Combine(path1, fileName), config);
                }
                catch { file1 = null; }
                try
                {
                    file2 = await readFileService.GetJsonDocument(Path.Combine(path2, fileName), config);
                }
                catch { file2 = null; }

                result.Add(analyzeService.Compare(file1, file2));
                reportProgress(progress, new ProgressReport(result.Count, fileNames.Count));
            }

            Console.WriteLine();
            Console.WriteLine("Done");
            Trace.Unindent();
            return result;
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
