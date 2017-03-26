namespace BigEgg.Tools.JsonComparer.JsonDocuments
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
        private readonly IList<KeyValuePair<string, FieldType>> fieldInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDocument" /> class.
        /// </summary>
        /// <param name="fileName">The file name</param>
        public JsonDocument(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) { throw new ArgumentException("fileName"); }

            FileName = fileName;
            this.fieldInfos = new List<KeyValuePair<string, FieldType>>();

            properties = new Dictionary<string, Property>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDocument" /> class.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="fieldInfos">The fields information.</param>
        public JsonDocument(string fileName, IList<KeyValuePair<string, FieldType>> fieldInfos)
        {
            if (string.IsNullOrWhiteSpace(fileName)) { throw new ArgumentException("fileName"); }
            if (fieldInfos == null) { throw new ArgumentNullException("fieldInfos"); }

            FileName = fileName;
            this.fieldInfos = fieldInfos;

            properties = new Dictionary<string, Property>();
        }


        /// <summary>
        /// Gets the file name.
        /// </summary>
        /// <value>
        /// The the file name.
        /// </value>
        public string FileName { get; private set; }

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
        /// Creates a specified property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <exception cref="ArgumentException">propertyName</exception>
        public void CreateProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) { throw new ArgumentException("propertyName"); }

            if (!this.properties.ContainsKey(propertyName))
            {
                Property property = fieldInfos.Count == 0
                    ? new Property(propertyName)
                    : new Property(propertyName, fieldInfos);

                this.properties.Add(propertyName, property);
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
