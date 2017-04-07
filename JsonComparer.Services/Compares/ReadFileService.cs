namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.Linq;
    using System.IO;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    using BigEgg.Tools.JsonComparer.JsonDocuments;
    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;
    using BigEgg.Tools.JsonComparer.Services.Json;

    [Export(typeof(IReadFileService))]
    internal class ReadFileService : IReadFileService
    {
        private readonly IJsonDocumentService jsonDocumentService;
        private readonly IDictionary<FieldType, JTokenType> supportJTokenTypes;


        /// <summary>
        /// Initializes a new instance of the <see cref="ReadFileService"/> class.
        /// </summary>
        /// <param name="jsonDocumentService">The JSON document service.</param>
        [ImportingConstructor]
        public ReadFileService(IJsonDocumentService jsonDocumentService)
        {
            this.jsonDocumentService = jsonDocumentService;

            supportJTokenTypes = new Dictionary<FieldType, JTokenType>
            {
                { FieldType.Boolean, JTokenType.Boolean },
                { FieldType.Integer, JTokenType.Integer },
                { FieldType.String, JTokenType.String }
            };
        }


        public async Task<JsonDocument> GetJsonDocument(string fileName, CompareConfigDocument config)
        {
            Trace.Indent();
            Trace.TraceInformation($"Start to read data in file {fileName}");

            var jsonFile = jsonDocumentService.ReadJsonFile(fileName);
            var node = jsonDocumentService.GetNode(jsonFile, config.StartNodeName) as JObject;
            if (node == null) { throw new NotSupportedException($"Cannot find JSON node {config.StartNodeName} in {fileName}"); }

            Trace.TraceInformation("Reading properties in document");

            var document = new JsonDocument(
                Path.GetFileNameWithoutExtension(fileName),
                config.FieldInfos.Select(item => new KeyValuePair<string, FieldType>(item.FieldName, item.FieldType)).ToList());

            await AnalyzeJsonDocument(node, document, config, node.Path.Remove(node.Path.Length - config.StartNodeName.Length)); ;

            Trace.Unindent();
            return document;
        }


        private Task AnalyzeJsonDocument(JObject node, JsonDocument document, CompareConfigDocument config, string paddingString)
        {
            return Task.Factory.StartNew(() =>
            {
                var queue = new Queue<JToken>();
                queue.Enqueue(node);

                do
                {
                    var item = queue.Dequeue();
                    if (item.Type == JTokenType.Array)
                    {
                        (item as JArray).Children()
                                        .Where(child => child.Type == JTokenType.Object)
                                        .ToList()
                                        .ForEach(child => queue.Enqueue(child as JObject));
                    }
                    else if (item.Type == JTokenType.Object)
                    {
                        (item as JObject).Properties().ToList().ForEach(property =>
                        {
                            var newNode = AnalyzeNode(property, document, config, paddingString);
                            if (newNode != null)
                            {
                                queue.Enqueue(newNode);
                            }
                        });
                    }
                } while (queue.Count != 0);
            });
        }

        private JToken AnalyzeNode(JProperty node, JsonDocument document, CompareConfigDocument config, string paddingString)
        {
            if (!node.HasValues) { return null; }

            var path = GetThePath(node.Path, paddingString, config.PropertyNodesName);

            if (node.Value.Type == JTokenType.Object)
            {
                if (!config.PropertyNodesName.Contains(node.Name))
                {
                    document.CreateProperty(path);
                    foreach (var fieldInfo in config.FieldInfos)
                    {
                        document[path][fieldInfo.FieldName] = fieldInfo.DefaultValue.ToString();
                    }
                }

                return node.Value;
            }
            else if (node.Value.Type == JTokenType.Array)
            {
                return node.Value;
            }
            else if (supportJTokenTypes.Values.Contains(node.Value.Type))
            {
                path = path.Replace($".{node.Name}", "");

                var info = config.FieldInfos.FirstOrDefault(fieldInfo => fieldInfo.FieldName.Equals(node.Name));
                if (info == null) { return null; }

                if (info.NeedReplace && info.ReplaceValue.ToString().Equals(node.Value.ToString()))
                {
                    document[path].UpdateValue(node.Name, info.DefaultValue.ToString());
                }
                else if (supportJTokenTypes[info.FieldType] == node.Value.Type)
                {
                    document[path].UpdateValue(node.Name, node.Value.ToString());
                }
            }

            return null;
        }

        private string GetThePath(string path, string paddingString, IList<string> propertyNodesName)
        {
            path = path.Replace(paddingString, "");
            foreach (var propertyNodeName in propertyNodesName)
            {
                path = path.Replace($"{propertyNodeName}.", "");
            }
            return path;
        }
    }
}
