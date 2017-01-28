namespace BigEgg.Tools.JsonComparer.CompareData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The property inforamtion of the JSON Document
    /// </summary>
    public class CompareItem
    {
        private readonly IDictionary<string, CompareValue> oldData;
        private readonly IDictionary<string, CompareValue> newData;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareItem" /> class.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="oldData">The old data.</param>
        /// <param name="newData">The new data.</param>
        /// <exception cref="System.ArgumentException">propertyName cannot be null or empty.</exception>
        /// <exception cref="System.ArgumentNullException">data1 and data2 cannot be null at sametime.</exception>
        internal CompareItem(string propertyName, IList<CompareValue> oldData, IList<CompareValue> newData)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) { throw new ArgumentException("propertyName"); }
            if (oldData == null && newData == null) { throw new ArgumentNullException("data"); }

            PropertyName = propertyName;
            if (oldData != null)
            {
                HasOldData = true;
                this.oldData = oldData.ToDictionary(item => item.Name, item => item);
            }
            if (newData != null)
            {
                HaveData2 = true;
                this.newData = newData.ToDictionary(item => item.Name, item => item);
            }
        }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        /// <value>
        /// The property name.
        /// </value>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether have old data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if have old data; otherwise, <c>false</c>.
        /// </value>
        public bool HasOldData { get; private set; }

        /// <summary>
        /// Gets a value indicating whether have new data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if have new data; otherwise, <c>false</c>.
        /// </value>
        public bool HaveData2 { get; private set; }

        /// <summary>
        /// Gets the old data.
        /// </summary>
        /// <value>
        /// The old data.
        /// </value>
        public IDictionary<string, CompareValue> OldData { get { return oldData; } }

        /// <summary>
        /// Gets the new data.
        /// </summary>
        /// <value>
        /// The new data.
        /// </value>
        public IDictionary<string, CompareValue> NewData { get { return newData; } }
    }
}
