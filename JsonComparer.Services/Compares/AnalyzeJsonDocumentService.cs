namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    using BigEgg.Tools.JsonComparer.JsonDocuments;
    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;
    using BigEgg.Tools.JsonComparer.Services.Json;

    [Export(typeof(IAnalyzeJsonDocumentService))]
    internal class AnalyzeJsonDocumentService : IAnalyzeJsonDocumentService
    {
        private readonly IJsonDocumentService jsonDocumentService;

        private readonly IDictionary<FieldType, JTokenType> supportJTokenTypes;


        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzeJsonDocumentService"/> class.
        /// </summary>
        /// <param name="jsonDocumentService">The json service.</param>
        [ImportingConstructor]
        public AnalyzeJsonDocumentService(IJsonDocumentService jsonDocumentService)
        {
            this.jsonDocumentService = jsonDocumentService;

            supportJTokenTypes = new Dictionary<FieldType, JTokenType>();
            supportJTokenTypes.Add(FieldType.Boolean, JTokenType.Boolean);
            supportJTokenTypes.Add(FieldType.Integer, JTokenType.Integer);
            supportJTokenTypes.Add(FieldType.String, JTokenType.String);
        }


        public async Task<JsonDocument> GetJsonDocument(string fileName, CompareConfigDocument config)
        {
            Trace.Indent();
            Trace.TraceInformation($"Start to read data in file {fileName}");

            var jsonFile = jsonDocumentService.ReadJsonFile(fileName);
            if (jsonFile == null) { throw new NotSupportedException($"Cannot read JSON file {fileName}"); }

            var node = jsonDocumentService.GetNode(jsonFile, config.StartNodeName) as JObject;
            if (node == null) { throw new NotSupportedException($"Cannot find JSON node {config.StartNodeName} in {fileName}"); }

            Trace.TraceInformation("Reading properties in document");

            var document = config.FieldInfos == null
                ? new JsonDocument(fileName)
                : new JsonDocument(fileName, config.FieldInfos.Select(item => new KeyValuePair<string, FieldType>(item.FieldName, item.FieldType)).ToList());

            document.CreateProperty(config.StartNodeName);
            var paddingString = node.Path.Remove(node.Path.Length - config.StartNodeName.Length);

            await AnalyzeDocument(node, document, config, paddingString); ;

            Trace.Unindent();
            return document;
        }

        private Task AnalyzeDocument(JObject node, JsonDocument document, CompareConfigDocument config, string paddingString)
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
                    if (config.FieldInfos != null)
                    {
                        foreach (var fieldInfo in config.FieldInfos)
                        {
                            document[path][fieldInfo.FieldName] = fieldInfo.DefaultValue.ToString();
                        }
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

                if (config.FieldInfos == null)
                {
                    document[path].UpdateValue(DefaultField.DEFAULT_FIELD_NAME, node.Value.ToString());
                }
                else
                {
                    var info = config.FieldInfos.FirstOrDefault(fieldInfo => fieldInfo.FieldName.Equals(node.Name));
                    if (info.NeedReplace && info.ReplaceValue.ToString().Equals(node.Value.ToString()))
                    {
                        document[path].UpdateValue(node.Name, info.DefaultValue.ToString());
                    }
                    else if (supportJTokenTypes[info.FieldType] == node.Value.Type)
                    {
                        document[path].UpdateValue(node.Name, node.Value.ToString());
                    }
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

        private JToken HandleObject(JProperty node, JsonDocument document, CompareConfigDocument config, string path)
        {
            if (!config.PropertyNodesName.Contains(node.Name))
            {
                document.CreateProperty(path);

                config.FieldInfos?.ToList().ForEach(fieldInfo =>
                {
                    document[path][fieldInfo.FieldName] = fieldInfo.DefaultValue.ToString();
                });
            }

            return node.Value;
        }
    }
}
