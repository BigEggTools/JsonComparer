namespace BigEgg.Tools.JsonComparer.JsonDocument
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// The JSON Document
    /// </summary>
    public class JsonDocument : IEnumerable<KeyValuePair<string, Property>>
    {
        private readonly IDictionary<string, Property> properties;
        private IList<KeyValuePair<string, FieldType>> fieldInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDocument" /> class.
        /// </summary>
        /// <param name="fieldInfos">The fields information.</param>
        public JsonDocument(IList<KeyValuePair<string, FieldType>> fieldInfos)
        {
            if (fieldInfos == null) { throw new ArgumentNullException("fieldInfos"); }

            this.fieldInfos = fieldInfos;

            properties = new Dictionary<string, Property>();
        }

        /// <summary>
        /// Creates a specified propery.
        /// </summary>
        /// <param name="properyName">The propery name.</param>
        /// <exception cref="ArgumentException">properyName</exception>
        public void Create(string properyName)
        {
            if (string.IsNullOrWhiteSpace(properyName)) { throw new ArgumentException("properyName"); }

            if (!this.properties.ContainsKey(properyName))
            {
                Property property = fieldInfos.Count == 0
                    ? new Property(properyName)
                    : new Property(properyName, fieldInfos);

                this.properties.Add(properyName, property);
            }
        }

        /// <summary>
        /// Gets the <see cref="Property"/> with the property name.
        /// </summary>
        /// <value>
        /// The <see cref="Property"/>.
        /// </value>
        /// <param name="propertyName">The property name.</param>
        /// <returns></returns>
        public Property this[string propertyName]
        {
            get
            {
                return properties.ContainsKey(propertyName)
                    ? properties[propertyName]
                    : null;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<string, Property>> GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return properties.GetEnumerator();
        }
    }
}
