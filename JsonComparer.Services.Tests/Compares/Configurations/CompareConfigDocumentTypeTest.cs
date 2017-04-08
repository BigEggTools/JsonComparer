namespace BigEgg.Tools.JsonComparer.Services.Tests.Compares.Configurations
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.UnitTesting;

    using BigEgg.Tools.JsonComparer.JsonDocuments;
    using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;

    public class CompareConfigDocumentTypeTest
    {
        private const string JSON_CONFIG_FILE = "TestData\\Compares\\Configurations\\json\\config.json";
        private const string JSON_CONFIG_FILE_1 = "TestData\\Compares\\Configurations\\json\\config_1.json";
        private const string JSON_CONFIG_FILE_INVALID_FOLDER = "TestData\\Compares\\Configurations\\json\\invalid";
        private const string OTHER_CONFIG_FILE = "TestData\\Compares\\Configurations\\config.other";

        [TestClass]
        public class ReadFromJson : TestClassBase
        {
            [TestMethod]
            public void Path_PreconditionCheck()
            {
                var service = Container.GetExportedValue<ICompareConfigDocumentType>();

                AssertHelper.ExpectedException<ArgumentException>(() => service.Read(null));
                AssertHelper.ExpectedException<ArgumentException>(() => service.Read(string.Empty));
                AssertHelper.ExpectedException<ArgumentException>(() => service.Read("    "));
            }

            [TestMethod]
            public void FileNotExist()
            {
                var service = Container.GetExportedValue<ICompareConfigDocumentType>();
                AssertHelper.ExpectedException<FileNotFoundException>(() => service.Read("notExist.json"));
            }

            [TestMethod]
            public void ExistFile()
            {
                var service = Container.GetExportedValue<ICompareConfigDocumentType>();
                var result = service.Read(JSON_CONFIG_FILE);
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

            [TestMethod]
            public void NoFieldInfos_Null()
            {
                //  TODO: will have a feature later to allow user don't pass the field infos
                var service = Container.GetExportedValue<ICompareConfigDocumentType>();
                var result = service.Read(JSON_CONFIG_FILE_1);
                Assert.IsNull(result);
            }

            [TestMethod]
            public void InvalidConfig()
            {
                var service = Container.GetExportedValue<ICompareConfigDocumentType>();

                var files = Directory.EnumerateFiles(JSON_CONFIG_FILE_INVALID_FOLDER);
                foreach (var file in files)
                {
                    var result = service.Read(file);
                    Assert.IsNull(result, $"failed on test data: {file}");
                }
            }
        }

        [TestClass]
        public class ReadFromOthers : TestClassBase
        {
            [TestMethod]
            public void UnsupportedFileExtension()
            {
                var service = Container.GetExportedValue<ICompareConfigDocumentType>();
                var result = service.Read(OTHER_CONFIG_FILE);
                Assert.IsNull(result);
            }
        }
    }
}
