namespace BigEgg.Tools.JsonComparer.JsonDocument
{
    using System;

    /// <summary>
    /// The field informantion of the JSON Document Property
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Field" /> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="type">The type of the field.</param>
        public Field(string name, FieldType type = FieldType.String)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("name"); }
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the value the field.
        /// </summary>
        /// <value>
        /// The value of the field.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets the type of the field.
        /// </summary>
        /// <value>
        /// The type of the field.
        /// </value>
        public FieldType Type { get; private set; }
    }
}
