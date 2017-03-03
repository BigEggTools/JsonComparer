namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    using Newtonsoft.Json;

    using BigEgg.Tools.JsonComparer.JsonDocument;

    internal class CompareFieldConfig
    {
        [JsonProperty(Required = Required.Always)]
        public string FieldName { get; private set; }

        [JsonProperty(Required = Required.Always)]
        public FieldType FieldType { get; private set; }

        [JsonProperty(Required = Required.Always)]
        public object DefaultValue { get; private set; }

        public object ReplaceValue { get; private set; }

        public bool NeedReplace { get { return ReplaceValue != null; } }
    }
}
