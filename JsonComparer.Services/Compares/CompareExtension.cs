namespace BigEgg.Tools.JsonComparer.Services.Compare
{
    using System;
    using System.Linq;

    using BigEgg.Tools.JsonComparer.CompareData;
    using BigEgg.Tools.JsonComparer.JsonDocument;

    /// <summary>
    /// The extension class for compare data model
    /// </summary>
    internal static class CompareExtension
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
        /// <exception cref="ArgumentNullException">Parameter property2 should not be null.</exception>
        /// <exception cref="NotSupportedException">Should not compare to property with different name.</exception>
        public static CompareItem ToCompareWithAnother(this Property property1, Property property2, Func<string, string> replaceValue)
        {
            Preconditions.NotNull(property2, "property2");

            if (!property1.Name.Equals(property2.Name, StringComparison.InvariantCulture)) { throw new NotSupportedException("Should not compare to property with different name."); }

            return new CompareItem(property1.Name,
                property1.Select(item => item.Value.ToCompare(replaceValue)).ToList(),
                property2.Select(item => item.Value.ToCompare(replaceValue)).ToList());
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
                property.Select(item => item.Value.ToCompare(replaceValue)).ToList(),
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
                property.Select(item => item.Value.ToCompare(replaceValue)).ToList());
        }
    }
}
