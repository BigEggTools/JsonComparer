namespace BigEgg.Tools.JsonComparer.Services.Tests.Compares
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BigEgg.UnitTesting;

    using BigEgg.Tools.JsonComparer.JsonDocuments;
    using BigEgg.Tools.JsonComparer.Services.Compares;
    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;

    public class AnalyzeJsonDocumentServiceTest
    {
        private const string JSON_FILE = "TestData\\Compares\\article.json";


        [TestClass]
        public class GetJsonDocument : TestClassBase
        {
            [TestMethod]
            public async Task FileNotExist()
            {
                var service = Container.GetExportedValue<IAnalyzeJsonDocumentService>();
                await AssertHelper.ExpectedExceptionAsync<FileNotFoundException>(() => service.GetJsonDocument("NotExist", GetCompareConfigDocument()));
            }

            [TestMethod]
            public async Task CannotFindNode()
            {
                var service = Container.GetExportedValue<IAnalyzeJsonDocumentService>();
                var config = GetCompareConfigDocument();
                config.StartNodeName = "NotExist";

                await AssertHelper.ExpectedExceptionAsync<NotSupportedException>(() => service.GetJsonDocument(JSON_FILE, config));
            }

            [TestMethod]
            public async Task GetDocument()
            {
                var service = Container.GetExportedValue<IAnalyzeJsonDocumentService>();
                var config = GetCompareConfigDocument();
                var document = await service.GetJsonDocument(JSON_FILE, config);

                Assert.IsNotNull(document);
                Assert.AreEqual("article", document.FileName);

                foreach (var property in document)
                {
                    Assert.IsFalse(string.IsNullOrWhiteSpace(property.Key));

                    Assert.IsNotNull(property.Value);
                    Assert.AreEqual(property.Key, property.Value.Name);

                    foreach (var field in property.Value)
                    {
                        Assert.IsFalse(string.IsNullOrWhiteSpace(field.Key));

                        Assert.IsNotNull(field.Value);
                        Assert.AreEqual(field.Key, field.Value.Name);

                        var fieldConfig = config.FieldInfos.FirstOrDefault(x => x.FieldName == field.Value.Name);
                        Assert.IsNotNull(fieldConfig);
                        Assert.AreEqual(fieldConfig.FieldType, field.Value.Type);
                        Assert.IsFalse(string.IsNullOrWhiteSpace(field.Value.Value));
                    }
                }

            }


            private CompareConfigDocument GetCompareConfigDocument()
            {
                return new CompareConfigDocument()
                {
                    StartNodeName = "_document",
                    PropertyNodesName = new List<string>() { "properties", "fields" },
                    FieldInfos = new List<CompareFieldConfig>()
                    {
                        new CompareFieldConfig() { FieldName = "type", FieldType = FieldType.String, DefaultValue = "string" },
                        new CompareFieldConfig() { FieldName = "include_in_all", FieldType = FieldType.Boolean, DefaultValue = "DEFAULT", ReplaceValue = false },
                        new CompareFieldConfig() { FieldName = "analyzer", FieldType = FieldType.String, DefaultValue = "DEFAULT", ReplaceValue = "standard" },
                    }
                };
            }
        }
    }
}
