namespace BigEgg.Tools.JsonComparer.Services.Tests.Compares.Configurations
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.JsonComparer.JsonDocument;
    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;

    public class CompareConfigDocumentTest
    {
        private const string JSON_CONFIG_FILE = "TestData\\Compares\\Configurations\\config.json";
        private const string OTHER_CONFIG_FILE = "TestData\\Compares\\Configurations\\config.other";

        [TestClass]
        public class ReadFromJson : TestClassBase
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Path_Null()
            {
                var service = Container.GetExportedValue<ICompareConfigDocument>();
                service.ReadFromFile(null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Path_EmptyString()
            {
                var service = Container.GetExportedValue<ICompareConfigDocument>();
                service.ReadFromFile(string.Empty);
            }

            [TestMethod]
            [ExpectedException(typeof(FileNotFoundException))]
            public void FileNotExist()
            {
                var service = Container.GetExportedValue<ICompareConfigDocument>();
                service.ReadFromFile("notExist.json");
            }

            [TestMethod]
            public void ExistFile()
            {
                var service = Container.GetExportedValue<ICompareConfigDocument>();
                var result = service.ReadFromFile(JSON_CONFIG_FILE);
                Assert.IsNotNull(result);

                Assert.AreEqual("_document", result.StartNodeName);
                Assert.AreEqual(2, result.PropertyNodesName.Count);
                Assert.AreEqual("properties", result.PropertyNodesName[0]);
                Assert.AreEqual(3, result.FieldInfos.Count);
                Assert.AreEqual("type", result.FieldInfos[0].FieldName);
                Assert.AreEqual("include_in_all", result.FieldInfos[1].FieldName);
                Assert.AreEqual("analyzer", result.FieldInfos[2].FieldName);
                Assert.AreEqual(FieldType.Boolean, result.FieldInfos[1].FieldType);
                Assert.AreEqual(false, result.FieldInfos[1].DefaultValue);
                Assert.AreEqual("default", result.FieldInfos[1].ReplaceValue);
            }
        }

        [TestClass]
        public class ReadFromOthers : TestClassBase
        {
            [TestMethod]
            public void UnsupportedFileExtension()
            {
                var service = Container.GetExportedValue<ICompareConfigDocument>();
                var result = service.ReadFromFile(OTHER_CONFIG_FILE);
                Assert.IsNull(result);
            }
        }
    }
}
