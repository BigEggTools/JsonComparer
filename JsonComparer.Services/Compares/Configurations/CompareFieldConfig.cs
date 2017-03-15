namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    using BigEgg.Tools.JsonComparer.JsonDocument;

    internal class CompareFieldConfig : ValidatableObject
    {
        [JsonProperty(Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public string FieldName { get; private set; }

        [JsonProperty(Required = Required.Always)]
        public FieldType FieldType { get; private set; }

        [JsonProperty(Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public object DefaultValue { get; private set; }

        public object ReplaceValue { get; private set; }

        public bool NeedReplace { get { return ReplaceValue != null; } }
    }
}
