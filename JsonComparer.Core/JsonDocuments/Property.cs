namespace BigEgg.Tools.JsonComparer.JsonDocuments
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The property information of the JSON Document
    /// </summary>
    public class Property : IEnumerable<KeyValuePair<string, Field>>
    {
        private readonly IDictionary<string, Field> fields;

        /// <summary>
        /// Initializes a new instance of the <see cref="Property" /> class.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <exception cref="System.ArgumentException">The name cannot be null or empty.</exception>
        public Property(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("name"); }
            Name = name;

            fields = new Dictionary<string, Field>();
            fields.Add(DefaultField.DEFAULT_FIELD_NAME, new DefaultField());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class with fields information.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="fieldInfos">The fields information.</param>
        /// <exception cref="System.ArgumentException">The name cannot be null or empty.</exception>
        /// <exception cref="ArgumentNullException">FieldInfos cannot be null.</exception>
        public Property(string name, IList<KeyValuePair<string, FieldType>> fieldInfos)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("name"); }
            if (fieldInfos == null || fieldInfos.Count < 1) { throw new ArgumentNullException("fieldInfos"); }

            Name = name;
            fields = fieldInfos.ToDictionary(item => item.Key, item => new Field(item.Key, item.Value));
        }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Updates the value of the field.
        /// </summary>
        /// <param name="fieldName">The field name.</param>
        /// <param name="value">The value of field.</param>
        /// <exception cref="ArgumentException">
        /// fieldName
        /// or
        /// value
        /// </exception>
        public void UpdateValue(string fieldName, string value)
        {
            if (string.IsNullOrWhiteSpace(fieldName)) { throw new ArgumentException("fieldName"); }
            if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("value"); }

            if (this.fields.ContainsKey(fieldName))
            {
                this.fields[fieldName].Value = value;
            }
        }

        /// <summary>
        /// Updates the value of the field.
        /// </summary>
        /// <param name="fieldName">The field name.</param>
        /// <param name="value">The value of field.</param>
        /// <exception cref="ArgumentException">
        /// fieldName
        /// or
        /// value
        /// </exception>
        public void UpdateValue(string fieldName, bool value)
        {
            UpdateValue(fieldName, value.ToString().ToUpper());
        }

        /// <summary>
        /// Updates the value of the field.
        /// </summary>
        /// <param name="fieldName">The field name.</param>
        /// <param name="value">The value of field.</param>
        /// <exception cref="ArgumentException">
        /// fieldName
        /// or
        /// value
        /// </exception>
        public void UpdateValue(string fieldName, int value)
        {
            UpdateValue(fieldName, value.ToString());
        }

        /// <summary>
        /// Gets or sets the value with the specified field name.
        /// </summary>
        /// <value>
        /// The value of field.
        /// </value>
        /// <param name="fieldName">The field name.</param>
        /// <returns>THe value of field.</returns>
        public string this[string fieldName]
        {
            get
            {
                return fields.ContainsKey(fieldName)
                    ? fields[fieldName].Value
                    : string.Empty;
            }
            set
            {
                UpdateValue(fieldName, value);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<string, Field>> GetEnumerator()
        {
            return fields.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return fields.GetEnumerator();
        }
    }
}
