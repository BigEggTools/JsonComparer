namespace BigEgg.Tools.JsonComparer.Services.Compares.Configurations
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    using BigEgg.Tools.JsonComparer.JsonDocuments;

    /// <summary>
    /// The configuration model for compare field
    /// </summary>
    /// <seealso cref="BigEgg.ValidatableObject" />
    public class CompareFieldConfig : ValidatableObject
    {
        /// <summary>
        /// Gets the field name.
        /// </summary>
        /// <value>
        /// The field name.
        /// </value>
        [JsonProperty(Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public string FieldName { get; internal set; }

        /// <summary>
        /// Gets the field type.
        /// </summary>
        /// <value>
        /// The field type.
        /// </value>
        [JsonProperty(Required = Required.Always)]
        public FieldType FieldType { get; internal set; }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        [JsonProperty(Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public object DefaultValue { get; internal set; }

        /// <summary>
        /// Gets the replace value.
        /// </summary>
        /// <value>
        /// The replace value.
        /// </value>
        public object ReplaceValue { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the value need to check to be replace.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value need to check to be replace; otherwise, <c>false</c>.
        /// </value>
        public bool NeedReplace { get { return ReplaceValue != null; } }
    }
}
