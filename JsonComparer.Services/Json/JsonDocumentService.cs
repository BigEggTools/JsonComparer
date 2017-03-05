namespace BigEgg.Tools.JsonComparer.Services.Json
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The base logic for reading, writing JSON file, and find the JSON node in JSON.
    /// </summary>
    [Export(typeof(IJsonDocumentService))]
    public class JsonDocumentService : IJsonDocumentService
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
        public JToken ReadJsonFile(string path)
        {
            Preconditions.NotNullOrWhiteSpace(path, "path");

            Trace.Indent();
            Trace.TraceInformation($"Start to read data in file {path}");

            using (StreamReader sr = new StreamReader(path))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                var result = JToken.ReadFrom(reader);

                Trace.TraceWarning(result == null
                    ? $"Failed to read data in file {path}"
                    : $"End to read data in file {path}");
                Trace.Unindent();

                return result;
            }
        }

        /// <summary>
        /// Writes the <see cref="Newtonsoft.Json.Linq.JToken"/> object to a json file.
        /// </summary>
        /// <param name="data">The JSON data.</param>
        /// <param name="path">The file path.</param>
        /// <exception cref="System.UnauthorizedAccessException">Access is denied.</exception>
        /// <exception cref="System.ArgumentNullException">Data is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// Path is an empty string ("") or null.
        /// or
        /// Path contains the name of a system device (com1, com2, and so on).
        /// </exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters.</exception>
        /// <exception cref="System.IO.IOException">Path includes an incorrect or invalid syntax for file name, directory name, or volume label syntax.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        public void WriteJsonFile(JToken data, string path)
        {
            Preconditions.NotNull(data, "data");
            Preconditions.NotNullOrWhiteSpace(path, "path");

            Trace.Indent();
            Trace.TraceInformation($"Start to write data to file {path}");

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
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
        public JToken GetNode(JToken jsonObject, string nodeName, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            Preconditions.NotNull(jsonObject, "jsonObject");
            Preconditions.NotNullOrWhiteSpace(nodeName, "nodeName");

            Trace.Indent();
            Trace.TraceInformation($"Start Finding '{nodeName}' node");

            var queue = new Queue<JToken>();
            queue.Enqueue(jsonObject);

            JToken result = null;
            do
            {
                var node = queue.Dequeue();
                switch (node.Type)
                {
                    case JTokenType.Object:
                        result = FindNodeInJObject((JObject)node, nodeName, comparisonType, queue);
                        break;
                    case JTokenType.Array:
                        result = FindNodeInJArray((JArray)node, nodeName, comparisonType, queue);
                        break;
                }

                if (result != null) { queue.Clear(); }
            } while (queue.Count != 0);

            Trace.TraceWarning(result == null
                ? $"Failed to get '{nodeName}' node in file"
                : $"End Finding '{nodeName}' node");
            Trace.Unindent();

            return result;
        }

        private JToken FindNodeInJObject(JObject node, string nodeName, StringComparison comparisonType, Queue<JToken> queue)
        {
            var properties = node.Properties();

            foreach (var property in properties)
            {
                if (property.Name.Equals(nodeName, comparisonType)) { return property.Value; }

                if (property.HasValues &&
                    (property.Value.Type == JTokenType.Object ||
                     property.Value.Type == JTokenType.Array))
                {
                    queue.Enqueue(property.Value);
                }
            }
            return null;
        }

        private JToken FindNodeInJArray(JArray node, string nodeName, StringComparison comparisonType, Queue<JToken> queue)
        {
            var children = node.Children();

            foreach (var child in children)
            {
                if (child.HasValues &&
                    (child.Type == JTokenType.Object ||
                     child.Type == JTokenType.Array))
                {
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    }
}
