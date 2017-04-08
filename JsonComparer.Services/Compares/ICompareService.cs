namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BigEgg.Progress;

    using BigEgg.Tools.JsonComparer.CompareData;

    /// <summary>
    /// The logic for compare JSON files in 2 folder.
    /// </summary>
    public interface ICompareService
    {
        /// <summary>
        /// Compares JSON files in 2 folder and output the result.
        /// </summary>
        /// <param name="path1">The directory path of JSON files.</param>
        /// <param name="path2">The another directory path of JSON files.</param>
        /// <param name="configFile">The configuration file.</param>
        /// <param name="progress">The progress update provider.</param>
        /// <returns>All compare result</returns>
        Task<IList<CompareFile>> Compare(string path1, string path2, string configFile, IProgress<IProgressReport> progress = null);
    }
}
