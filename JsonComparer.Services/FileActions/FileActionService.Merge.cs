namespace BigEgg.Tools.JsonComparer.Services.FileActions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BigEgg.Progress;
    using Newtonsoft.Json.Linq;

    public partial class FileActionService
    {
        /// <summary>
        /// Merges the files.
        /// </summary>
        /// <param name="fileName">The JSON file name to be the root.</param>
        /// <param name="nodeName">The root node name for merging.</param>
        /// <param name="filesPath">The split JSON files path.</param>
        /// <param name="progress">The progress update provider.</param>
        /// <returns></returns>
        /// <exception cref="System.UnauthorizedAccessException">Access is denied.</exception>
        /// <exception cref="System.ArgumentNullException">Data is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// fileName is an empty string ("") or null.
        /// or
        /// fileName contains the name of a system device (com1, com2, and so on).
        /// or
        /// nodeName is an empty string ("") or null.
        /// or
        /// filesPath is an empty string ("") or null.
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
        /// Cannot merge to JSON node
        /// or
        /// Cannot find folder
        /// or
        /// Cannot find small JSON files to merge in folder
        /// </exception>
        public async Task MergeFiles(string fileName, string nodeName, string filesPath, IProgress<IProgressReport> progress = null)
        {
            Preconditions.NotNullOrWhiteSpace(fileName, "fileName");
            Preconditions.NotNullOrWhiteSpace(nodeName, "nodeName");
            Preconditions.NotNullOrWhiteSpace(filesPath, "filesPath");

            Trace.Indent();
            Trace.TraceInformation($"Start merge JSON data in file {fileName}");

            JObject jsonFile = (JObject)jsonDocumentService.ReadJsonFile(fileName);
            if (jsonFile == null) { throw new NotSupportedException($"Cannot read JSON file {fileName}"); }

            var node = jsonDocumentService.GetNode(jsonFile, nodeName);
            if (node == null) { throw new NotSupportedException($"Cannot find JSON node {nodeName} in {fileName}"); }

            if (!Directory.Exists(filesPath)) { throw new NotSupportedException($"Cannot find folder {filesPath}"); }
            var files = Directory.EnumerateFiles(filesPath).Where(file => Path.GetExtension(file).ToLower() == ".json").ToList();
            if (!files.Any()) { throw new NotSupportedException($"Cannot find small JSON files to merge in folder {filesPath}"); }

            switch (node.Type)
            {
                case JTokenType.Array:
                    await MergeArray((JArray)node, nodeName, files, progress);
                    break;
                case JTokenType.Object:
                    await MergeObject((JObject)node, nodeName, files, progress);
                    break;
                default:
                    throw new NotSupportedException($"Cannot merge to JSON node {nodeName} in {fileName}");
            }

            Trace.TraceInformation($"Merge JSON data in file {fileName} complete.");
            Trace.Unindent();
        }

        private Task MergeObject(JObject node, string nodeName, IList<string> files, IProgress<IProgressReport> progress)
        {
            return Task.Factory.StartNew(() =>
            {
                Trace.Indent();
                Trace.TraceInformation($"Start merging properties in '{nodeName}' node");

                var length = files.Count();
                for (int i = 0; i < length; i++)
                {
                    reportProgress(progress, new ProgressReport(i + 1, length));
                    var file = files[i];

                    try
                    {
                        JObject jsonFile = (JObject)jsonDocumentService.ReadJsonFile(file);
                        node.Add(Path.GetFileNameWithoutExtension(file), jsonFile);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! Skipped.");
                        Trace.TraceError($"Cannot read JSON file {file}, skipped. Error message: ${ex.Message}");
                        continue;
                    }
                }
                reportProgress(progress, new ProgressReport(length, length));

                Trace.TraceInformation($"Merge Complete");
                Trace.Unindent();
            });
        }

        private Task MergeArray(JArray node, string nodeName, IList<string> files, IProgress<IProgressReport> progress)
        {
            return Task.Factory.StartNew(() =>
            {
                Trace.Indent();
                Trace.TraceInformation($"Start merging array items in '{nodeName}' node");

                var length = files.Count();
                for (int i = 0; i < length; i++)
                {
                    reportProgress(progress, new ProgressReport(i + 1, length));
                    var file = files[i];

                    try
                    {
                        JObject jsonFile = (JObject)jsonDocumentService.ReadJsonFile(file);
                        node.Add(jsonFile);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! Skipped.");
                        Trace.TraceError($"Cannot read JSON file {file}, skipped. Error message: ${ex.Message}");
                        continue;
                    }
                }
                reportProgress(progress, new ProgressReport(length, length));

                Trace.TraceInformation($"Merge Complete");
                Trace.Unindent();
            });
        }
    }
}
