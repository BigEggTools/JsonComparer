namespace BigEgg.Tools.JsonComparer.CompareData
{
    using System;

    using BigEgg.Tools.JsonComparer.JsonDocument;

    internal static class FieldExtension
    {
        public static CompareValue ToCompareValue(this Field field, Func<string, string> replaceValue)
        {
            return new CompareValue(field.Name, replaceValue(field.Value));
        }
    }
}
