namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Newtonsoft.Json;
    using BigEgg.Validations;

    internal class CompareConfig : ValidatableObject
    {
        [JsonProperty(Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public string StartNodeName { get; private set; }

        [JsonProperty(Required = Required.Always)]
        [MinimumElements(1)]
        public IList<string> PropertyNodesName { get; private set; }

        [JsonProperty(Required = Required.Always)]
        [MinimumElements(1)]
        public IList<CompareFieldConfig> FieldInfos { get; private set; }


        public override IEnumerable<ValidationResult> Validate()
        {
            return base.Validate().Concat(FieldInfos.SelectMany(fieldConfig => fieldConfig.Validate()));
        }
    }
}
