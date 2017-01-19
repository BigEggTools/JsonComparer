namespace BigEgg.Tools.JsonComparer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The base logic for reading, writing JSON file, and find the JSON node in JSON.
    /// </summary>
    public abstract class JsonDocumentServiceBase
    {
        /// <summary>
        /// Reads the json file and return a <see cref="Newtonsoft.Json.Linq.JObject"/> object.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns>The <see cref="Newtonsoft.Json.Linq.JObject"/> object.</returns>
        /// <exception cref="System.ArgumentException">Path is an empty string ("") or null.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file cannot be found.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">Path includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
        protected JObject ReadJsonFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) { throw new ArgumentException("path"); }

            Trace.Indent();
            Trace.TraceInformation($"Start to read data in file {path}");

            using (StreamReader sr = new StreamReader(path))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                var result = (JObject)JToken.ReadFrom(reader);
                if (result == null)
                {
                    Trace.TraceWarning($"Failed to read data in file {path}");
                }

                Trace.TraceInformation($"End to read data in file {path}");
                Trace.Unindent();
                return result;
            }
        }

        /// <summary>
        /// Writes the <see cref="Newtonsoft.Json.Linq.JObject"/> object to a json file.
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
        protected void WriteJsonFile(JObject data, string path)
        {
            if (data == null) { throw new ArgumentNullException("data"); }
            if (string.IsNullOrWhiteSpace(path)) { throw new ArgumentException("path"); }

            Trace.Indent();
            Trace.TraceInformation($"Start to write data to file {path}");

            using (StreamWriter sw = new StreamWriter(path))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                data.WriteTo(writer);

                sw.Close();

                Trace.TraceInformation($"End to read data in file {path}");
                Trace.Unindent();
            }
        }

        /// <summary>
        /// Gets the JSON node from a <see cref="Newtonsoft.Json.Linq.JContainer" /> object.
        /// </summary>
        /// <param name="jsonObject">The root json object.</param>
        /// <param name="nodeName">The node's name.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies how the strings will be compared.</param>
        /// <returns>
        /// The <see cref="Newtonsoft.Json.Linq.JContainer" /> object, <c>null</c> if not found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">JsonObject is null</exception>
        /// <exception cref="System.ArgumentException">NodeName is null or empty string.</exception>
        protected JToken GetNode(JObject jsonObject, string nodeName, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            if (jsonObject == null) { throw new ArgumentNullException("jsonObject"); }
            if (string.IsNullOrWhiteSpace(nodeName)) { throw new ArgumentException("nodeName"); }

            Trace.Indent();
            Trace.TraceInformation($"Start Finding '{nodeName}' node");

            var queue = new Queue<JObject>();
            queue.Enqueue(jsonObject);

            do
            {
                var node = queue.Dequeue();
                var properties = node.Properties();

                foreach (var property in properties)
                {
                    if (property.Name.Equals(nodeName, comparisonType))
                    {
                        queue.Clear();

                        var result = property.Value;

                        Trace.TraceInformation($"End Finding '{nodeName}' node");
                        Trace.Unindent();

                        return result;
                    }
                    if (property.HasValues && property.Value.Type == JTokenType.Object)
                    {
                        queue.Enqueue((JObject)property.Value);
                    }
                }
            } while (queue.Count != 0);

            Trace.TraceWarning($"Failed to get '{nodeName}' node in file");
            Trace.Unindent();
            return null;
        }
    }
}
