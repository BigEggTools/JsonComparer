namespace BigEgg.Tools.JsonComparer.CompareData
{
    using System;

    using BigEgg.Tools.JsonComparer.JsonDocument;

    /// <summary>
    /// The extension class for compare data model
    /// </summary>
    public static class CompareExtension
    {
        /// <summary>
        /// To the compare value.
        /// </summary>
        /// <param name="field1">The field1.</param>
        /// <param name="field2">The field2.</param>
        /// <param name="replaceValue">The replace value function.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException">
        /// fields' name or type is not same
        /// </exception>
        public static CompareValue ToCompareValue(this Field field1, Field field2, Func<string, string> replaceValue)
        {
            if (!field1.Name.Equals(field2.Name)) { throw new NotSupportedException("fields' name is not same"); }
            if (!field1.Type.Equals(field2.Type)) { throw new NotSupportedException("fields' type is not same"); }
            return new CompareValue(field1.Name, replaceValue(field1.Value), replaceValue(field1.Value));
        }
    }
}
