﻿namespace BigEgg.Tools.JsonComparer.Reports.Excel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using BigEgg.Tools.JsonComparer.CompareData;
    using BigEgg.Tools.JsonComparer.Reports.Excel.Configurations;

    /// <summary>
    /// The Excel report output logic for compare JSON file results.
    /// </summary>
    /// <seealso cref="BigEgg.Tools.JsonComparer.Reports.IReportService" />
    [Export(typeof(IReportService))]
    internal class ExcelReportService : ReportServiceBase
    {
        private const string splitReportFileNameFormat = "report_{0}.xlsx";
        private const string reportFileNameFormat = "report.xlsx";
        private readonly IExcelReportDocumentType documentType;


        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelReportService"/> class.
        /// </summary>
        [ImportingConstructor]
        public ExcelReportService(IExcelReportDocumentType documentType)
            : base("Excel", true)
        {
            this.documentType = documentType;
        }


        /// <summary>
        /// Outputs the core.
        /// </summary>
        /// <param name="compareFiles">The compare files.</param>
        /// <param name="path1">The path1.</param>
        /// <param name="path2">The path2.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="split">if set to <c>true</c> [split].</param>
        /// <returns></returns>
        protected override Task OutputCore(IList<CompareFile> compareFiles, string path1, string path2, string outputPath, bool split)
        {
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            var config = new ExcelReportConfigurationDocument();

            Console.WriteLine($"Start Export the Report of the Compared Files. Total Number: {compareFiles.Count}.");
            if (split)
            {
                Console.WriteLine($"Report will be Split.");
                return Task.Factory.StartNew(() =>
                {
                    Trace.Indent();
                    foreach (var file in compareFiles)
                    {
                        Trace.TraceInformation($"Output report file for {file.FileName}.");
                        var document = documentType.New();
                        document.NewSheet(file, path1, path2, config);

                        var reportFileName = Path.Combine(outputPath, string.Format(splitReportFileNameFormat, file.FileName));
                        documentType.Save(document, reportFileName);
                    }
                    Trace.Unindent();
                });
            }
            else
            {
                return Task.Factory.StartNew(() =>
                {
                    Trace.Indent();
                    var document = documentType.New();

                    foreach (var file in compareFiles)
                    {
                        Trace.TraceInformation($"Output report file for {file.FileName}.");
                        document.NewSheet(file, path1, path2, config);
                    }

                    var reportFileName = Path.Combine(outputPath, reportFileNameFormat);
                    documentType.Save(document, reportFileName);
                    Trace.Unindent();
                });
            }
        }
    }
}
