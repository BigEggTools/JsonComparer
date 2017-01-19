namespace BigEgg.Tools.JsonComparer.JsonDocument
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// The property inforamtion of the JSON Document
    /// </summary>
    public class Property : IEnumerable<KeyValuePair<string, Field>>
    {
        private readonly IDictionary<string, Field> fields;

        /// <summary>
        /// Initializes a new instance of the <see cref="Property" /> class.
        /// </summary>
        public Property()
        {
            fields = new Dictionary<string, Field>();
            fields.Add(DefaultField.DEFAULT_FIELD_NAME, new DefaultField());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class with fields information.
        /// </summary>
        /// <param name="fieldInfos">The fields information.</param>
        /// <exception cref="ArgumentNullException">fieldInfos</exception>
        public Property(IList<KeyValuePair<string, FieldType>> fieldInfos)
        {
            if (fieldInfos == null || fieldInfos.Count < 1) { throw new ArgumentNullException("fieldInfos"); }

            fields = new Dictionary<string, Field>();
            foreach (var fieldInfo in fieldInfos)
            {
                fields.Add(fieldInfo.Key, new Field(fieldInfo.Key, fieldInfo.Value));
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
