﻿namespace BigEgg.Tools.JsonComparer.CompareData
{
    using System;
    using System.Linq;

    using BigEgg.Tools.JsonComparer.JsonDocument;

    /// <summary>
    /// The extension class for compare data model
    /// </summary>
    public static class CompareExtension
    {
        /// <summary>
        /// To the compare value model.
        /// </summary>
        /// <param name="field">The field1.</param>
        /// <param name="replaceValue">The replace value function.</param>
        /// <returns>To compare value model.</returns>
        public static CompareValue ToCompare(this Field field, Func<string, string> replaceValue = null)
        {
            return replaceValue == null
                ? new CompareValue(field.Name, field.Value)
                : new CompareValue(field.Name, replaceValue(field.Value));
        }

        /// <summary>
        /// To the compare item model.
        /// </summary>
        /// <param name="property1">The property1.</param>
        /// <param name="property2">The property2.</param>
        /// <param name="replaceValue">The replace value function.</param>
        /// <returns>The compare item model</returns>
        public static CompareItem ToCompareWithAnother(this Property property1, Property property2, Func<string, string> replaceValue)
        {
            if (property2 == null) { throw new ArgumentNullException("property2"); }
            if (!property1.Name.Equals(property2.Name, StringComparison.InvariantCulture)) { throw new NotSupportedException(""); }

            return new CompareItem(property1.Name,
                property1.Select(item => item.Value.ToCompare()).ToList(),
                property2.Select(item => item.Value.ToCompare()).ToList());
        }

        /// <summary>
        /// To the compare item model with only old data.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="replaceValue">The replace value function.</param>
        /// <returns>The compare item model.</returns>
        public static CompareItem ToCompareForOld(this Property property, Func<string, string> replaceValue)
        {
            return new CompareItem(property.Name,
                property.Select(item => item.Value.ToCompare()).ToList(),
                null);
        }

        /// <summary>
        /// To the compare item model with only new data.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="replaceValue">The replace value function.</param>
        /// <returns>The compare item model.</returns>
        public static CompareItem ToCompareForNew(this Property property, Func<string, string> replaceValue)
        {
            return new CompareItem(property.Name,
                null,
                property.Select(item => item.Value.ToCompare()).ToList());
        }
    }
}