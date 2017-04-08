namespace BigEgg.Tools.JsonComparer.CompareData
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The file compare information of the JSON Document
    /// </summary>
    public class CompareFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareFile" /> class.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="compareItems">The compare items form file1 and file2.</param>
        /// <exception cref="System.ArgumentException">fileName cannot be null or empty.</exception>
        /// <exception cref="System.ArgumentNullException">compareItems cannot be null or empty.</exception>
        public CompareFile(string fileName, IList<CompareItem> compareItems)
        {
            if (string.IsNullOrWhiteSpace(fileName)) { throw new ArgumentException("fileName cannot be null or empty."); }
            if (compareItems == null && compareItems.Count == 0) { throw new ArgumentNullException("compareItems cannot be null or empty."); }

            FileName = fileName;
            CompareItems = compareItems;
        }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        /// <value>
        /// The file name.
        /// </value>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the compare items form file1 and file2.
        /// </summary>
        /// <value>
        /// The compare items form file1 and file2.
        /// </value>
        public IList<CompareItem> CompareItems { get; private set; }
    }
}
