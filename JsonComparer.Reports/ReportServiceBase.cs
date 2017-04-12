namespace BigEgg.Tools.JsonComparer.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BigEgg.Tools.JsonComparer.CompareData;

    /// <summary>
    /// The base report service
    /// </summary>
    /// <seealso cref="BigEgg.Tools.JsonComparer.Reports.IReportService" />
    public abstract class ReportServiceBase : IReportService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportServiceBase"/> class.
        /// </summary>
        /// <param name="name">The name of the report.</param>
        /// <param name="supportSplitOutputFiles">if set to <c>true</c> this report service support split the output reports.</param>
        public ReportServiceBase(string name, bool supportSplitOutputFiles)
        {
            Name = name;
            SupportSplitOutputFiles = supportSplitOutputFiles;
        }


        /// <summary>
        /// Gets the name of the report.
        /// </summary>
        /// <value>
        /// The name of the report.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this report service support split the output reports.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this report service support split the output reports; otherwise, <c>false</c>.
        /// </value>
        public bool SupportSplitOutputFiles { get; private set; }

        /// <summary>
        /// Outputs the compare files to that path.
        /// </summary>
        /// <param name="compareFiles">The compare files.</param>
        /// <param name="path1">The directory path of JSON files.</param>
        /// <param name="path2">The another directory path of JSON files.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="split">if set to <c>true</c> [split].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Parameter 'compareFiles' should not be null or empty list.
        /// or
        /// Parameter 'path1' should not be empty string.
        /// or
        /// Parameter 'path2' should not be empty string.
        /// or
        /// Parameter 'outputPath' should not be empty string.
        /// or
        /// Path1 should not be same as Path2
        /// </exception>
        public Task Output(IList<CompareFile> compareFiles, string path1, string path2, string outputPath, bool split)
        {
            if (compareFiles == null || compareFiles.Count == 0) { throw new ArgumentException("Parameter 'compareFiles' should not be null or empty list."); }
            if (string.IsNullOrWhiteSpace(path1)) { throw new ArgumentException("Parameter 'path1' should not be empty string."); }
            if (string.IsNullOrWhiteSpace(path2)) { throw new ArgumentException("Parameter 'path2' should not be empty string."); }
            if (string.IsNullOrWhiteSpace(outputPath)) { throw new ArgumentException("Parameter 'outputPath' should not be empty string."); }
            if (path1.Equals(path2, StringComparison.OrdinalIgnoreCase)) { throw new ArgumentException("Path1 should not be same as Path2"); }

            return OutputCore(compareFiles, path1, path2, outputPath, split);
        }

        /// <summary>
        /// Outputs the compare files to that path.
        /// </summary>
        /// <param name="compareFiles">The compare files.</param>
        /// <param name="path1">The directory path of JSON files.</param>
        /// <param name="path2">The another directory path of JSON files.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="split">if set to <c>true</c> [split].</param>
        /// <returns></returns>
        protected abstract Task OutputCore(IList<CompareFile> compareFiles, string path1, string path2, string outputPath, bool split);
    }
}
