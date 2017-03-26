namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Newtonsoft.Json;
    using BigEgg.Validations;

    /// <summary>
    /// The document model for compare configuration
    /// </summary>
    /// <seealso cref="BigEgg.ValidatableObject" />
    public class CompareConfigDocument : ValidatableObject
    {
        /// <summary>
        /// Gets the name of the start node.
        /// </summary>
        /// <value>
        /// The name of the start node.
        /// </value>
        [JsonProperty(Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public string StartNodeName { get; private set; }

        /// <summary>
        /// Gets the name of the property nodes.
        /// </summary>
        /// <value>
        /// The name of the property nodes.
        /// </value>
        [JsonProperty(Required = Required.Always)]
        [MinimumElements(1)]
        public IList<string> PropertyNodesName { get; private set; }

        /// <summary>
        /// Gets the field infos.
        /// </summary>
        /// <value>
        /// The field infos.
        /// </value>
        [JsonProperty(Required = Required.DisallowNull)]
        [MinimumElements(1)]
        public IList<CompareFieldConfig> FieldInfos { get; private set; }


        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        public override IEnumerable<ValidationResult> Validate()
        {
            if (FieldInfos == null) { return base.Validate(); }

            return base.Validate().Concat(FieldInfos.SelectMany(fieldConfig => fieldConfig.Validate()));
        }
    }
}
