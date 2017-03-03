namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    internal class CompareConfig
    {
        [JsonProperty(Required = Required.Always)]
        public string StartNodeName { get; private set; }

        [JsonProperty(Required = Required.Always)]
        public IList<string> PropertyNodesName { get; private set; }

        [JsonProperty(Required = Required.Always)]
        public IList<CompareFieldConfig> FieldInfos { get; private set; }
    }
}
