using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace JsonComparer.Services.Tests
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
        public class ReadJsonFileTest
        {
            private IJsonDocumentService service = new JsonDocumentService();

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Path_Null()
            {
                var result = service.ReadJsonFile(null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Path_EmptyString()
            {
                var result = service.ReadJsonFile(string.Empty);
            }

            [TestMethod]
            [ExpectedException(typeof(FileNotFoundException))]
            public void FileNotExist()
            {
                var result = service.ReadJsonFile("notExist.json");
            }

            [TestMethod]
            public void ExistFile()
            {
                var result = service.ReadJsonFile(TEST_JSON_FILE);
                Assert.IsNotNull(result);
            }
        }

        [TestClass]
        public class WriteJsonFileTest
        {
            private const string NEW_FILE_PATH = "TestData\\NewJson.json";
            private IJsonDocumentService service = new JsonDocumentService();

            [TestCleanup]
            public void TestCleanup()
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
                service.WriteJsonFile(null, NEW_FILE_PATH);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Path_Null()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                service.WriteJsonFile(jsonObject, null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Path_Empty()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                service.WriteJsonFile(jsonObject, string.Empty);
            }

            [TestMethod]
            public void FileNotExist()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                service.WriteJsonFile(jsonObject, NEW_FILE_PATH);
                Assert.IsTrue(File.Exists(NEW_FILE_PATH));

                var newJsonObject = service.ReadJsonFile(NEW_FILE_PATH);
                Assert.IsNotNull(newJsonObject);
            }

            [TestMethod]
            public void FileExisted()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                service.WriteJsonFile(jsonObject, NEW_FILE_PATH);
                Assert.IsTrue(File.Exists(NEW_FILE_PATH));

                service.WriteJsonFile(jsonObject, NEW_FILE_PATH);
                var newJsonObject = service.ReadJsonFile(NEW_FILE_PATH);
                Assert.IsNotNull(newJsonObject);
            }
        }

        [TestClass]
        public class GetNodeTest
        {
            private const string VERSION_NODE_NAME = "version";
            private const string DATA_NODE_NAME = "data";
            private const string DATA_AGE_NODE_NAME = "age";
            private const string STATUS_NODE_NAME = "status";
            private const string STATUS_PROGESS_NODE_NAME = "progress";
            private IJsonDocumentService service = new JsonDocumentService();

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Data_Null()
            {
                service.GetNode(null, DATA_NODE_NAME);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void NodeName_Null()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void NodeName_EmptyString()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, string.Empty);
            }

            [TestMethod]
            public void NodeExist_Array()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, DATA_NODE_NAME);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(JArray));
                Assert.AreEqual(2, ((JArray)result).Count);
            }

            [TestMethod]
            public void NodeExist_Token()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, VERSION_NODE_NAME);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(JValue));
                Assert.AreEqual("v1", ((JValue)result).Value);
            }

            [TestMethod]
            public void NodeExist_Object()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, STATUS_NODE_NAME);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(JObject));
                Assert.AreEqual(2, ((JObject)result).Properties().Count());
            }

            [TestMethod]
            public void NodeExist_InObject()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, STATUS_PROGESS_NODE_NAME);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(JValue));
                Assert.AreEqual(1L, ((JValue)result).Value);
            }

            [TestMethod]
            public void NodeExist_InArray()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, DATA_AGE_NODE_NAME);
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(JValue));
                Assert.AreEqual(30L, ((JValue)result).Value);
            }

            [TestMethod]
            public void NodeNotExist()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, "NotExist");
                Assert.IsNull(result);
            }

            [TestMethod]
            public void NodeCaseNotMatch()
            {
                var jsonObject = service.ReadJsonFile(TEST_JSON_FILE);

                var result = service.GetNode(jsonObject, DATA_NODE_NAME.ToUpper(), StringComparison.Ordinal);
                Assert.IsNull(result);
            }
        }
    }
}
