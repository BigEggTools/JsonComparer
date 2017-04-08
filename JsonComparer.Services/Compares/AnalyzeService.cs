namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using BigEgg.Tools.JsonComparer.CompareData;
    using BigEgg.Tools.JsonComparer.JsonDocuments;
    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;

    [Export(typeof(IAnalyzeService))]
    internal class AnalyzeService : IAnalyzeService
    {
        public CompareFile Compare(JsonDocument document1, JsonDocument document2)
        {
            Preconditions.Check(document1 != null || document2 != null, "Parameter 'document1' and 'document2' cannot be null at same time");

            return null;
        }


        #region Property to Compare Item
        private CompareItem CompareWithAnother(Property property1, Property property2, IList<CompareFieldConfig> fieldConfigs)
        {
            Preconditions.NotNull(property1, "property1");
            Preconditions.NotNull(property2, "property2");
            Preconditions.NotNull(fieldConfigs, "fieldConfigs");
            Preconditions.Check<NotSupportedException>(property1.Name.Equals(property2.Name, StringComparison.InvariantCulture), "Should not compare to property with different name.");
            Preconditions.Check<ArgumentException>(fieldConfigs.Count > 0);

            return CompareItem.WithBothData(property1.Name,
                property1.Select(item =>
                {
                    var setting = fieldConfigs.First(config => config.FieldName.Equals(item.Key));
                    return FieldToCompare(item.Value, setting.DefaultValue, setting.ReplaceValue);
                }).ToList(),
                property2.Select(item =>
                {
                    var setting = fieldConfigs.First(config => config.FieldName.Equals(item.Key));
                    return FieldToCompare(item.Value, setting.DefaultValue, setting.ReplaceValue);
                }).ToList());
        }

        private CompareItem CompareWithNothing(Property property, IList<CompareFieldConfig> fieldConfigs)
        {
            Preconditions.NotNull(property, "property1");
            Preconditions.NotNull(fieldConfigs, "fieldConfigs");
            Preconditions.Check<ArgumentException>(fieldConfigs.Count > 0);

            return CompareItem.WithData1(property.Name, property.Select(item =>
            {
                var setting = fieldConfigs.First(config => config.FieldName.Equals(item.Key));
                return FieldToCompare(item.Value, setting.DefaultValue, setting.ReplaceValue);
            }).ToList());
        }

        private CompareItem NothingWithCompareData(Property property, IList<CompareFieldConfig> fieldConfigs)
        {
            Preconditions.NotNull(property, "property1");
            Preconditions.NotNull(fieldConfigs, "fieldConfigs");
            Preconditions.Check<ArgumentException>(fieldConfigs.Count > 0);

            return CompareItem.WithData2(property.Name, property.Select(item =>
            {
                var setting = fieldConfigs.First(config => config.FieldName.Equals(item.Key));
                return FieldToCompare(item.Value, setting.DefaultValue, setting.ReplaceValue);
            }).ToList());
        }
        #endregion

        private CompareValue FieldToCompare(Field field, object defaultValue, object replaceValue)
        {
            return new CompareValue(field.Name,
                string.IsNullOrWhiteSpace(field.Value) || (replaceValue != null && field.Value.Equals(replaceValue.ToString()))
                    ? defaultValue.ToString()
                    : field.Value);
        }
    }
}
