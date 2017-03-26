namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BigEgg.Progress;

    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;
    using BigEgg.Tools.JsonComparer.Services.Json;
    using JsonDocuments;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// The logic for compare JSON files in 2 folder.
    /// </summary>
    [Export(typeof(ICompareService))]
    internal class CompareService : ICompareService
    {
        private readonly ICompareConfigDocumentType configDocumentType;
        private readonly IAnalyzeJsonDocumentService analyzeJsonDocumentService;

        private readonly IDictionary<FieldType, JTokenType> supportJTokenTypes;


        /// <summary>
        /// Initializes a new instance of the <see cref="CompareService"/> class.
        /// </summary>
        /// <param name="configDocumentType">Type of the configuration document.</param>
        /// <param name="analyzeJsonDocumentService">The analyze json service.</param>
        [ImportingConstructor]
        public CompareService(ICompareConfigDocumentType configDocumentType, IAnalyzeJsonDocumentService analyzeJsonDocumentService)
        {
            this.configDocumentType = configDocumentType;
            this.analyzeJsonDocumentService = analyzeJsonDocumentService;

            supportJTokenTypes = new Dictionary<FieldType, JTokenType>();
            supportJTokenTypes.Add(FieldType.Boolean, JTokenType.Boolean);
            supportJTokenTypes.Add(FieldType.Integer, JTokenType.Integer);
            supportJTokenTypes.Add(FieldType.String, JTokenType.String);
        }


        /// <summary>
        /// Compares JSON files in 2 folder and ouput the result.
        /// </summary>
        /// <param name="path1">The directory path of JSON files.</param>
        /// <param name="path2">The another directory path of JSON files.</param>
        /// <param name="configFile">The configuration file.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="progress">The progress update provider.</param>
        /// <returns></returns>
        public async Task Compare(string path1, string path2, string configFile, string outputPath, IProgress<IProgressReport> progress = null)
        {
            Preconditions.NotNullOrWhiteSpace(path1, "path1");
            Preconditions.NotNullOrWhiteSpace(path2, "path2");
            Preconditions.NotNullOrWhiteSpace(configFile, "configFile");
            Preconditions.NotNullOrWhiteSpace(outputPath, "outputPath");
            Preconditions.Check(path1.Equals(path2, StringComparison.OrdinalIgnoreCase), "Path1 should not be same as Path2");

            Trace.Indent();
            Trace.TraceInformation($"Start Read the Compare Config File: {configFile}.");
            var config = configDocumentType.Read(configFile);
            if (config == null) { throw new FileNotFoundException($"Cannot read Config File: {configFile}"); }

            Trace.TraceInformation($"Start Compare the folder {path1} with folder {path2}.");
            var filesFromPath1 = Directory.EnumerateFiles(path1).ToList();
            Trace.TraceInformation($"Have {filesFromPath1.Count} files from {path1}.");

            var filesFromPath2 = Directory.EnumerateFiles(path2).ToList();
            Trace.TraceInformation($"Have {filesFromPath2.Count} files from {path2}.");

            var totalNumber = Math.Max(filesFromPath1.Count, filesFromPath2.Count);
            Console.WriteLine($"Start compare the files. Total Number: {totalNumber}.");

            var i = 0;
            var j = 0;
            while (true)
            {
                if (i == filesFromPath1.Count)
                {
                    while (j < filesFromPath2.Count)
                    {
                        var file2 = analyzeJsonDocumentService.GetJsonDocument(filesFromPath2[j++], config);
                    }
                    break;
                }
                if (j == filesFromPath2.Count)
                {
                    while (i < filesFromPath1.Count)
                    {
                        var file1 = analyzeJsonDocumentService.GetJsonDocument(filesFromPath1[i++], config);
                    }
                    break;
                }

                var fileName1 = Path.GetFileName(filesFromPath1[i]);
                var fileName2 = Path.GetFileName(filesFromPath2[j]);
                var compareResult = fileName1.CompareTo(fileName2);
                if (compareResult == 0)
                {
                    var file1 = analyzeJsonDocumentService.GetJsonDocument(filesFromPath1[i++], config);
                    var file2 = analyzeJsonDocumentService.GetJsonDocument(filesFromPath2[j++], config);
                }
                else if (compareResult < 0)
                {
                    var file1 = analyzeJsonDocumentService.GetJsonDocument(filesFromPath1[i++], config);
                }
                else if (compareResult < 0)
                {
                    var file2 = analyzeJsonDocumentService.GetJsonDocument(filesFromPath2[j++], config);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Done");
            Trace.Unindent();
        }
    }
}
