﻿namespace BigEgg.Tools.JsonComparer.CompareData
{
    using System;

    /// <summary>
    /// The compare value
    /// </summary>
    public class CompareValue : IEquatable<CompareValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareValue"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentException">
        /// name
        /// or
        /// value
        /// </exception>
        public CompareValue(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("name"); }
            if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("value"); }

            Name = name;
            Value = value;
        }


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(CompareValue other)
        {
            return this.Value.Equals(other.Value);
        }
    }
}
