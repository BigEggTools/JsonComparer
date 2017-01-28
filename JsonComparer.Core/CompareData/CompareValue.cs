namespace BigEgg.Tools.JsonComparer.CompareData
{
    using System;

    /// <summary>
    /// The compare value
    /// </summary>
    public class CompareValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValue"/> class.
        /// </summary>
        /// <param name="name">The field name.</param>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The secend value.</param>
        /// <exception cref="System.ArgumentException">
        /// The field name, first value and second value cannot be null or empty
        /// </exception>
        public CompareValue(string name, string value1, string value2)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("name"); }
            if (string.IsNullOrWhiteSpace(value1)) { throw new ArgumentException("value1"); }
            if (string.IsNullOrWhiteSpace(value2)) { throw new ArgumentException("value2"); }

            Name = name;
            Value1 = value1;
            Value2 = value2;
        }


        /// <summary>
        /// Gets the field name.
        /// </summary>
        /// <value>
        /// The field name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the first value.
        /// </summary>
        /// <value>
        /// The first value.
        /// </value>
        public string Value1 { get; private set; }

        /// <summary>
        /// Gets the second value.
        /// </summary>
        /// <value>
        /// The second value.
        /// </value>
        public string Value2 { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the 2 value is equal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the 2 value is equal; otherwise, <c>false</c>.
        /// </value>
        public bool IsEqual { get { return Value1.Equals(Value2, StringComparison.InvariantCulture); } }
    }
}
