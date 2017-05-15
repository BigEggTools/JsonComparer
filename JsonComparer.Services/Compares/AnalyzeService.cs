namespace BigEgg.Tools.JsonComparer.Services.Compares
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading.Tasks;

    using BigEgg.Tools.JsonComparer.CompareData;
    using BigEgg.Tools.JsonComparer.JsonDocuments;

    [Export(typeof(IAnalyzeService))]
    internal class AnalyzeService : IAnalyzeService
    {
        public Task<CompareFile> Compare(JsonDocument document1, JsonDocument document2)
        {
            Preconditions.Check(document1 != null || document2 != null, "Parameter 'document1' and 'document2' cannot be null at same time.");
            Preconditions.Check(document1 == null || document2 == null || document1.FileName.Equals(document2.FileName), "2 documents should with same name.");

            var compareItems = new List<CompareItem>();

            if (document1 == null)
            {
                return Task.Factory.StartNew(() =>
                {
                    foreach (var property in document2)
                    {
                        compareItems.Add(NothingWithCompareData(property.Value));
                    };
                    return new CompareFile(document2.FileName, compareItems);
                });
            }

            if (document2 == null)
            {
                return Task.Factory.StartNew(() =>
                {
                    foreach (var property in document1)
                    {
                        compareItems.Add(CompareWithNothing(property.Value));
                    };
                    return new CompareFile(document1.FileName, compareItems);
                });
            }

            var properties1 = document1.Properties;
            var properties2 = document2.Properties;
            var i = 0; var j = 0;
            return Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (i == properties1.Count)
                    {
                        while (j < properties2.Count) { compareItems.Add(NothingWithCompareData(properties2[j++])); }
                        break;
                    }
                    if (j == properties2.Count)
                    {
                        while (i < properties1.Count) { compareItems.Add(CompareWithNothing(properties1[i++])); }
                        break;
                    }

                    var compareResult = properties1[i].Name.CompareTo(properties2[j].Name);
                    if (compareResult > 0) { compareItems.Add(NothingWithCompareData(properties2[j++])); }
                    else if (compareResult < 0) { compareItems.Add(CompareWithNothing(properties1[i++])); }
                    else { compareItems.Add(CompareWithAnother(properties1[i++], properties2[j++])); }
                }
                return new CompareFile(document1.FileName, compareItems);
            });
        }


        #region Property to Compare Item
        private CompareItem CompareWithAnother(Property property1, Property property2)
        {
            Preconditions.NotNull(property1, "property1");
            Preconditions.NotNull(property2, "property2");
            Preconditions.Check<NotSupportedException>(property1.Name.Equals(property2.Name, StringComparison.InvariantCulture), "Should not compare to property with different name.");

            return CompareItem.WithBothData(property1.Name,
                property1.Select(item => FieldToCompare(item.Value)).ToList(),
                property2.Select(item => FieldToCompare(item.Value)).ToList());
        }

        private CompareItem CompareWithNothing(Property property)
        {
            Preconditions.NotNull(property, "property");

            return CompareItem.WithData1(property.Name, property.Select(item => FieldToCompare(item.Value)).ToList());
        }

        private CompareItem NothingWithCompareData(Property property)
        {
            Preconditions.NotNull(property, "property");

            return CompareItem.WithData2(property.Name, property.Select(item => FieldToCompare(item.Value)).ToList());
        }
        #endregion

        private CompareValue FieldToCompare(Field field)
        {
            return new CompareValue(field.Name, field.Value);
        }
    }
}
