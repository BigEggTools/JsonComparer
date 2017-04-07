namespace BigEgg.Tools.JsonComparer.Services.Tests.Json
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json.Linq;
    using BigEgg.UnitTesting;

    using BigEgg.Tools.JsonComparer.Services.Json;

    public class JsonDocumentServiceTest
    {
        private const string TEST_JSON_FILE = "TestData\\SimpleJson.json";

        [TestClass]
        public class ReadJsonFileTest : TestClassBase
        {
            [TestMethod]
            public void Path_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                AssertHelper.ExpectedException<ArgumentException>(() => service.ReadJsonFile(null));
                AssertHelper.ExpectedException<ArgumentException>(() => service.ReadJsonFile(string.Empty));
                AssertHelper.ExpectedException<ArgumentException>(() => service.ReadJsonFile("    "));
            }

            [TestMethod]
            public void FileNotExist()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                AssertHelper.ExpectedException<FileNotFoundException>(() => service.ReadJsonFile("notExist.json"));
            }

            [TestMethod]
            public void ExistFile()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var document = service.ReadJsonFile(TEST_JSON_FILE);

                Assert.IsNotNull(document);
            }
        }

        [TestClass]
        public class WriteJsonFileTest : TestClassBase
        {
            private const string NEW_FILE_PATH = "out\\NewJson.json";

            protected override void OnTestCleanup()
            {
                if (File.Exists(NEW_FILE_PATH))
                {
                    File.Delete(NEW_FILE_PATH);
                }
            }

            [TestMethod]
            public void Data_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                AssertHelper.ExpectedException<ArgumentNullException>(() => service.WriteJsonFile(null, NEW_FILE_PATH));
            }

            [TestMethod]
            public void Path_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                AssertHelper.ExpectedException<ArgumentException>(() => service.WriteJsonFile(jsonObject, null));
                AssertHelper.ExpectedException<ArgumentException>(() => service.WriteJsonFile(jsonObject, string.Empty));
                AssertHelper.ExpectedException<ArgumentException>(() => service.WriteJsonFile(jsonObject, "    "));
            }

            [TestMethod]
            public void FileNotExist()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                service.WriteJsonFile(jsonObject, NEW_FILE_PATH);
                Assert.IsTrue(File.Exists(NEW_FILE_PATH));

                var newJsonObject = service.ReadJsonFile(NEW_FILE_PATH);
                Assert.IsNotNull(newJsonObject);
            }

            [TestMethod]
            public void FileExisted()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                service.WriteJsonFile(jsonObject, NEW_FILE_PATH);
                Assert.IsTrue(File.Exists(NEW_FILE_PATH));

                service.WriteJsonFile(jsonObject, NEW_FILE_PATH);
                var newJsonObject = service.ReadJsonFile(NEW_FILE_PATH);
                Assert.IsNotNull(newJsonObject);
            }
        }

        [TestClass]
        public class GetNodeTest : TestClassBase
        {
            private const string VERSION_NODE_NAME = "version";
            private const string DATA_NODE_NAME = "data";
            private const string DATA_AGE_NODE_NAME = "age";
            private const string STATUS_NODE_NAME = "status";
            private const string STATUS_PROGESS_NODE_NAME = "progress";

            [TestMethod]
            public void Data_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                AssertHelper.ExpectedException<ArgumentNullException>(() => service.GetNode(null, DATA_NODE_NAME));
            }

            [TestMethod]
            public void NodeName_Null()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                AssertHelper.ExpectedException<ArgumentException>(() => service.GetNode(jsonObject, null));
                AssertHelper.ExpectedException<ArgumentException>(() => service.GetNode(jsonObject, string.Empty));
                AssertHelper.ExpectedException<ArgumentException>(() => service.GetNode(jsonObject, "    "));
            }

            [TestMethod]
            public void NodeExist_Array()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, DATA_NODE_NAME);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(JArray));
                Assert.AreEqual(2, ((JArray)result).Count);
            }

            [TestMethod]
            public void NodeExist_Token()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, VERSION_NODE_NAME);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(JValue));
                Assert.AreEqual("v1", ((JValue)result).Value);
            }

            [TestMethod]
            public void NodeExist_Object()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, STATUS_NODE_NAME);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(JObject));
                Assert.AreEqual(2, ((JObject)result).Properties().Count());
            }

            [TestMethod]
            public void NodeExist_InObject()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, STATUS_PROGESS_NODE_NAME);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(JValue));
                Assert.AreEqual(1L, ((JValue)result).Value);
            }

            [TestMethod]
            public void NodeExist_InArray()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, DATA_AGE_NODE_NAME);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(JValue));
                Assert.AreEqual(30L, ((JValue)result).Value);
            }

            [TestMethod]
            public void NodeNotExist()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, "NotExist");
                Assert.IsNull(result);
            }

            [TestMethod]
            public void NodeCaseNotMatch()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, DATA_NODE_NAME.ToUpper(), StringComparison.Ordinal);
                Assert.IsNull(result);
            }
        }
    }
}
