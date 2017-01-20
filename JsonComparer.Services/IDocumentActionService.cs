namespace BigEgg.Tools.JsonComparer.Services
{
    using System;
    using System.Threading.Tasks;

    using BigEgg.Tools.JsonComparer.Progress;

    /// <summary>
    /// The logic for split JSON file or merge JSON files.
    /// </summary>
    public interface IDocumentActionService
    {
        /// <summary>
        /// Splits the file.
        /// </summary>
        /// <param name="fileName">The JSON file name.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="nodeName">The node name.</param>
        /// <param name="progress">The progress update provider.</param>
        /// <returns></returns>
        /// <exception cref="System.UnauthorizedAccessException">Access is denied.</exception>
        /// <exception cref="System.ArgumentNullException">Data is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// fileName is an empty string ("") or null.
        /// or
        /// fileName contains the name of a system device (com1, com2, and so on).
        /// or
        /// outputPath is an empty string ("") or null.
        /// or
        /// nodeName is an empty string ("") or null.
        /// </exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters.</exception>
        /// <exception cref="System.IO.IOException">Path includes an incorrect or invalid syntax for file name, directory name, or volume label syntax.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.NotSupportedException">
        /// Cannot read JSON file
        /// or
        /// Cannot find JSON node
        /// or
        /// Cannot Split JSON node
        /// </exception>
        Task SplitFile(string fileName, string outputPath, string nodeName, IProgress<IProgressReport> progress = null);

        /// <summary>
        /// Splits the file.
        /// </summary>
        /// <param name="fileName">The JSON file name.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="nodeName">The node name.</param>
        /// <param name="outputFileNamePattern">The output file name pattern.</param>
        /// <param name="progress">The progress update provider.</param>
        /// <returns></returns>
        /// <exception cref="System.UnauthorizedAccessException">Access is denied.</exception>
        /// <exception cref="System.ArgumentNullException">Data is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// fileName is an empty string ("") or null.
        /// or
        /// fileName contains the name of a system device (com1, com2, and so on).
        /// or
        /// outputPath is an empty string ("") or null.
        /// or
        /// nodeName is an empty string ("") or null.
        /// </exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters.</exception>
        /// <exception cref="System.IO.IOException">Path includes an incorrect or invalid syntax for file name, directory name, or volume label syntax.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.NotSupportedException">
        /// Cannot read JSON file
        /// or
        /// Cannot find JSON node
        /// or
        /// Cannot Split JSON node
        /// </exception>
        Task SplitFile(string fileName, string outputPath, string nodeName, string outputFileNamePattern, IProgress<IProgressReport> progress = null);
    }
}