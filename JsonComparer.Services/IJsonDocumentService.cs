namespace BigEgg.Tools.JsonComparer.Services
{
    using System;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The base logic for reading, writing JSON file, and find the JSON node in JSON.
    /// </summary>
    public interface IJsonDocumentService
    {
        /// <summary>
        /// Reads the json file and return a <see cref="Newtonsoft.Json.Linq.JToken"/> object.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns>The <see cref="Newtonsoft.Json.Linq.JToken"/> object.</returns>
        /// <exception cref="System.ArgumentException">Path is an empty string ("") or null.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file cannot be found.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">Path includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
        JToken ReadJsonFile(string path);

        /// <summary>
        /// Writes the <see cref="Newtonsoft.Json.Linq.JToken"/> object to a json file.
        /// </summary>
        /// <param name="data">The JSON data.</param>
        /// <param name="path">The file path.</param>
        /// <exception cref="System.UnauthorizedAccessException">Access is denied.</exception>
        /// <exception cref="System.ArgumentNullException">Data is null.</exception>
        /// <exception cref="System.ArgumentException">Path is an empty string ("") or null. -or-path contains the name of a system device (com1, com2, and so on).</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters.</exception>
        /// <exception cref="System.IO.IOException">Path includes an incorrect or invalid syntax for file name, directory name, or volume label syntax.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        void WriteJsonFile(JToken data, string path);

        /// <summary>
        /// Gets the JSON node from a <see cref="Newtonsoft.Json.Linq.JToken" /> object.
        /// </summary>
        /// <param name="jsonObject">The root json object.</param>
        /// <param name="nodeName">The node's name.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies how the strings will be compared.</param>
        /// <returns>
        /// The <see cref="Newtonsoft.Json.Linq.JContainer" /> object, <c>null</c> if not found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">JsonObject is null</exception>
        /// <exception cref="System.ArgumentException">NodeName is null or empty string.</exception>
        JToken GetNode(JToken jsonObject, string nodeName, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase);
    }
}
