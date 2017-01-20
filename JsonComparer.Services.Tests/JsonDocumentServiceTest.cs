namespace BigEgg.Tools.JsonComparer.Services.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json.Linq;

    using BigEgg.Tools.JsonComparer.Services;

    public class JsonDocumentServiceTest
    {
        private const string TEST_JSON_FILE = "TestData\\SimpleJson.json";

        [TestClass]
        public class ReadJsonFileTest : TestClassBase
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Path_Null()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var result = service.ReadJsonFile(null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Path_EmptyString()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var result = service.ReadJsonFile(string.Empty);
            }

            [TestMethod]
            [ExpectedException(typeof(FileNotFoundException))]
            public void FileNotExist()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var result = service.ReadJsonFile("notExist.json");
            }

            [TestMethod]
            public void ExistFile()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var result = service.ReadJsonFile(TEST_JSON_FILE);
                Assert.IsNotNull(result);
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
            [ExpectedException(typeof(ArgumentNullException))]
            public void Data_Null()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                service.WriteJsonFile(null, NEW_FILE_PATH);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Path_Null()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                service.WriteJsonFile(jsonObject, null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Path_Empty()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                service.WriteJsonFile(jsonObject, string.Empty);
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
            [ExpectedException(typeof(ArgumentNullException))]
            public void Data_Null()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                service.GetNode(null, DATA_NODE_NAME);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void NodeName_Null()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void NodeName_EmptyString()
            {
                var service = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, string.Empty);
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
