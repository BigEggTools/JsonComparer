namespace BigEgg.Tools.JsonComparer.CompareData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The property compare inforamtion of the JSON Document
    /// </summary>
    public class CompareItem
    {
        private CompareItem() { }


        /// <summary>
        /// Gets the property name.
        /// </summary>
        /// <value>
        /// The property name.
        /// </value>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether have data form file1.
        /// </summary>
        /// <value>
        ///   <c>true</c> if have data form file1; otherwise, <c>false</c>.
        /// </value>
        public bool HasData1 { get; private set; }

        /// <summary>
        /// Gets a value indicating whether have data form file2.
        /// </summary>
        /// <value>
        ///   <c>true</c> if have data form file2; otherwise, <c>false</c>.
        /// </value>
        public bool HasData2 { get; private set; }

        /// <summary>
        /// Gets the compare values form 2 files.
        /// </summary>
        /// <value>
        /// The compare values from 2 files.
        /// </value>
        public IDictionary<string, KeyValuePair<CompareValue, CompareValue>> Data { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="CompareItem" /> class.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="data1">The data form file1.</param>
        /// <exception cref="System.ArgumentException">propertyName cannot be null or empty.</exception>
        /// <exception cref="System.ArgumentNullException">data1 cannot be null at sametime.</exception>
        public static CompareItem WithData1(string propertyName, IList<CompareValue> data1)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) { throw new ArgumentException("propertyName cannot be null or empty."); }
            if (data1 == null) { throw new ArgumentNullException("data1 cannot be null at sametime."); }

            return new CompareItem()
            {
                PropertyName = propertyName,
                HasData1 = true,
                HasData2 = false,
                Data = data1.ToDictionary(item => item.Name, item => new KeyValuePair<CompareValue, CompareValue>(item, null))
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareItem" /> class.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="data2">The data form file2.</param>
        /// <exception cref="System.ArgumentException">propertyName cannot be null or empty.</exception>
        /// <exception cref="System.ArgumentNullException">data2 cannot be null at sametime.</exception>
        public static CompareItem WithData2(string propertyName, IList<CompareValue> data2)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) { throw new ArgumentException("propertyName cannot be null or empty."); }
            if (data2 == null) { throw new ArgumentNullException("data2 cannot be null at sametime."); }

            return new CompareItem()
            {
                PropertyName = propertyName,
                HasData1 = false,
                HasData2 = true,
                Data = data2.ToDictionary(item => item.Name, item => new KeyValuePair<CompareValue, CompareValue>(null, item))
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareItem" /> class.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="data1">The data form file1.</param>
        /// <param name="data2">The data from file2.</param>
        /// <exception cref="System.ArgumentException">propertyName cannot be null or empty.</exception>
        /// <exception cref="System.ArgumentNullException">data1 or data2 cannot be null.</exception>
        public static CompareItem WithBothData(string propertyName, IList<CompareValue> data1, IList<CompareValue> data2)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) { throw new ArgumentException("propertyName cannot be null or empty."); }
            if (data1 == null || data2 == null) { throw new ArgumentNullException("data1 or data2 cannot be null."); }
            if (data1.Select(item => item.Name).SequenceEqual(data2.Select(item => item.Name))) { throw new ArgumentException("data1, data2 should have same value sequence."); }

            var result = new CompareItem()
            {
                PropertyName = propertyName,
                HasData1 = true,
                HasData2 = true,
                Data = new Dictionary<string, KeyValuePair<CompareValue, CompareValue>>()
            };
            for (int i = 0; i < data1.Count; i++)
            {
                result.Data.Add(data1[i].Name, new KeyValuePair<CompareValue, CompareValue>(data1[i], data2[i]));
            }

            return result;
        }
    }
}
