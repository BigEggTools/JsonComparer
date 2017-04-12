namespace BigEgg.Tools.JsonComparer.Reports
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BigEgg.Tools.JsonComparer.CompareData;

    /// <summary>
    /// The report output logic for compare JSON file results.
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Gets the name of the report.
        /// </summary>
        /// <value>
        /// The name of the report.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether this report service support split the output reports.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this report service support split the output reports; otherwise, <c>false</c>.
        /// </value>
        bool SupportSplitOutputFiles { get; }

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
        Task Output(IList<CompareFile> compareFiles, string path1, string path2, string outputPath, bool split);
    }
}
