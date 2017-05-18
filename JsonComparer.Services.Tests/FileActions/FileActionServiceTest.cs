namespace BigEgg.Tools.JsonComparer.Services.Tests.FileActions
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Newtonsoft.Json.Linq;
    using BigEgg.Progress;
    using BigEgg.UnitTesting;

    using BigEgg.Tools.JsonComparer.Services.Json;
    using BigEgg.Tools.JsonComparer.Services.FileActions;

    public class FileActionServiceTest
    {
        [TestClass]
        public class SplitTest : TestClassBase
        {
            private const string TEST_JSON_FILE = "TestData\\Split\\JsonDocToSplit.json";
            private const string OUTPUT_PATH = "out";
            private const string VERSION_NODE_NAME = "version";
            private const string ARRAY_NODE_NAME = "array";
            private const string NUMBER_NODE_NAME = "number";
            private const string DATA_NODE_NAME = "data";
            private const string STATUS_NODE_NAME = "status";
            private readonly static string OutputFileNamePattern = $"name_{Constants.SPLIT_OUTPUT_FILE_NAME_REPLACER_NAME}_{Constants.SPLIT_OUTPUT_FILE_NAME_REPLACER_INDEX}";

            protected override void OnTestInitialize()
            {
                if (Directory.Exists(OUTPUT_PATH))
                {
                    Directory.Delete(OUTPUT_PATH, true);
                }
            }

            [TestMethod]
            public async Task FileName_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IFileActionService>();

                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.SplitFile(null, OUTPUT_PATH, DATA_NODE_NAME));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.SplitFile(string.Empty, OUTPUT_PATH, DATA_NODE_NAME));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.SplitFile("   ", OUTPUT_PATH, DATA_NODE_NAME));
            }

            [TestMethod]
            public async Task OutputPath_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.SplitFile(TEST_JSON_FILE, null, DATA_NODE_NAME));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.SplitFile(TEST_JSON_FILE, string.Empty, DATA_NODE_NAME));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.SplitFile(TEST_JSON_FILE, "    ", DATA_NODE_NAME));
            }

            [TestMethod]
            public async Task NodeName_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, null));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, string.Empty));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, "    "));
            }

            [TestMethod]
            public async Task SplitObjectInObject()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                await service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, DATA_NODE_NAME);

                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\BigEgg.json"));
                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\Pupil.json"));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\BigEgg.json");
                var valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(30L, ((JValue)valueNode).Value);
                jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\Pupil.json");
                valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(31L, ((JValue)valueNode).Value);
            }

            [TestMethod]
            public async Task SplitObjectInArray()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                await service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, ARRAY_NODE_NAME);

                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\1.json"));
                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\2.json"));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\1.json");
                var valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(30L, ((JValue)valueNode).Value);
                jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\2.json");
                valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(31L, ((JValue)valueNode).Value);
            }

            [TestMethod]
            public async Task SplitValue()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                await AssertHelper.ExpectedExceptionAsync<NotSupportedException>(() => service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, VERSION_NODE_NAME));
            }

            [TestMethod]
            public async Task SplitValueInObject()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                await service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, STATUS_NODE_NAME);

                Assert.AreEqual(0, Directory.EnumerateFiles(OUTPUT_PATH).Count());
            }

            [TestMethod]
            public async Task SplitValueInArray()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                await service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, STATUS_NODE_NAME);

                Assert.AreEqual(0, Directory.EnumerateFiles(OUTPUT_PATH).Count());
            }

            [TestMethod]
            public async Task SplitObjectInObject_WithFileNamePattern()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                await service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, DATA_NODE_NAME, OutputFileNamePattern);

                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\name_BigEgg_1.json"));
                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\name_Pupil_2.json"));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\name_BigEgg_1.json");
                var valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(30L, ((JValue)valueNode).Value);
                jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\name_Pupil_2.json");
                valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(31L, ((JValue)valueNode).Value);
            }

            [TestMethod]
            public async Task SplitObjectInArray_WithFileNamePattern()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                await service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, ARRAY_NODE_NAME, OutputFileNamePattern);

                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\name__1.json"));
                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\name__2.json"));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\name__1.json");
                var valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(30L, ((JValue)valueNode).Value);
                jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\name__2.json");
                valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(31L, ((JValue)valueNode).Value);
            }

            [TestMethod]
            public async Task SplitObjectInObject_WithProgress()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                var calledCount_SplitObjectInObject = 0;
                var progress = new Progress<IProgressReport>(report =>
                {
                    Assert.AreEqual(2, report.Total);
                    Assert.AreEqual(calledCount_SplitObjectInObject++, report.Current);
                });
                await service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, DATA_NODE_NAME, progress);

                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\BigEgg.json"));
                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\Pupil.json"));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\BigEgg.json");
                var valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(30L, ((JValue)valueNode).Value);
                jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\Pupil.json");
                valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(31L, ((JValue)valueNode).Value);
            }

            [TestMethod]
            public async Task SplitObjectInArray_WithProgress()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                var calledCount_SplitObjectInArray = 0;
                var progress = new Progress<IProgressReport>(report =>
                {
                    Assert.AreEqual(2, report.Total);
                    Assert.AreEqual(calledCount_SplitObjectInArray++, report.Current);
                });
                await service.SplitFile(TEST_JSON_FILE, OUTPUT_PATH, ARRAY_NODE_NAME, progress);

                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\1.json"));
                Assert.IsTrue(File.Exists($"{OUTPUT_PATH}\\2.json"));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\1.json");
                var valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(30L, ((JValue)valueNode).Value);
                jsonObj = documentSrv.ReadJsonFile($"{OUTPUT_PATH}\\2.json");
                valueNode = documentSrv.GetNode(jsonObj, "age");
                Assert.AreEqual(31L, ((JValue)valueNode).Value);
            }
        }

        [TestClass]
        public class MergeTest : TestClassBase
        {
            private const string TEST_JSON_FILE_ARRAY = "TestData\\Merge\\Array\\JsonDocToMerge.json";
            private const string TEST_JSON_FILE_ARRAY_ITEMS_FOLDER = "TestData\\Merge\\Array\\Items";
            private const string TEST_JSON_FILE_ARRAY_NEW = "TestData\\Merge\\Array\\JsonDocToMerge_New.json";
            private const string TEST_JSON_FILE_OBJECT = "TestData\\Merge\\Object\\JsonDocToMerge.json";
            private const string TEST_JSON_FILE_OBJECT_ITEMS_FOLDER = "TestData\\Merge\\Object\\Items";
            private const string TEST_JSON_FILE_OBJECT_NEW = "TestData\\Merge\\Object\\JsonDocToMerge_New.json";
            private const string VERSION_NODE_NAME = "version";
            private const string ARRAY_NODE_NAME = "array";
            private const string NUMBER_NODE_NAME = "number";
            private const string DATA_NODE_NAME = "data";
            private const string STATUS_NODE_NAME = "status";

            protected override void OnTestInitialize()
            {
                if (File.Exists(TEST_JSON_FILE_ARRAY_NEW))
                {
                    File.Delete(TEST_JSON_FILE_ARRAY_NEW);
                }
                if (File.Exists(TEST_JSON_FILE_OBJECT_NEW))
                {
                    File.Delete(TEST_JSON_FILE_OBJECT_NEW);
                }
            }


            [TestMethod]
            public async Task FileName_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IFileActionService>();

                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.MergeFiles(null, ARRAY_NODE_NAME, TEST_JSON_FILE_ARRAY_ITEMS_FOLDER));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.MergeFiles(string.Empty, ARRAY_NODE_NAME, TEST_JSON_FILE_ARRAY_ITEMS_FOLDER));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.MergeFiles("    ", ARRAY_NODE_NAME, TEST_JSON_FILE_ARRAY_ITEMS_FOLDER));
            }

            [TestMethod]
            public async Task NodeName_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IFileActionService>();

                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.MergeFiles(TEST_JSON_FILE_ARRAY, null, TEST_JSON_FILE_ARRAY_ITEMS_FOLDER));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.MergeFiles(TEST_JSON_FILE_ARRAY, string.Empty, TEST_JSON_FILE_ARRAY_ITEMS_FOLDER));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.MergeFiles(TEST_JSON_FILE_ARRAY, "    ", TEST_JSON_FILE_ARRAY_ITEMS_FOLDER));
            }

            [TestMethod]
            public async Task FilesPath_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IFileActionService>();

                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.MergeFiles(TEST_JSON_FILE_ARRAY, ARRAY_NODE_NAME, null));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.MergeFiles(TEST_JSON_FILE_ARRAY, ARRAY_NODE_NAME, string.Empty));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.MergeFiles(TEST_JSON_FILE_ARRAY, ARRAY_NODE_NAME, "    "));
            }

            [TestMethod]
            public async Task MergeObjectInArray_Empty()
            {
                var service = Container.GetExportedValue<IFileActionService>();

                await service.MergeFiles(TEST_JSON_FILE_ARRAY, ARRAY_NODE_NAME, TEST_JSON_FILE_ARRAY_ITEMS_FOLDER);

                Assert.IsTrue(File.Exists(TEST_JSON_FILE_ARRAY_NEW));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile(TEST_JSON_FILE_ARRAY_NEW);
                var node = documentSrv.GetNode(jsonObj, ARRAY_NODE_NAME);

                Assert.AreEqual(2, node.Children().Count());

                var bigeggNode = node.Children().First() as JObject;
                Assert.IsNotNull(bigeggNode);
                Assert.AreEqual("BigEgg", bigeggNode.Property("name").Value);
                Assert.AreEqual(30L, bigeggNode.Property("age").Value);
                Assert.AreEqual("Male", bigeggNode.Property("gender").Value);

                var pupilNode = node.Children().Last() as JObject;
                Assert.IsNotNull(pupilNode);
                Assert.AreEqual("Pupil", pupilNode.Property("name").Value);
                Assert.AreEqual(31L, pupilNode.Property("age").Value);
                Assert.AreEqual("Female", pupilNode.Property("gender").Value);
            }

            [TestMethod]
            public async Task MergeObjectInObject_Empty()
            {
                var service = Container.GetExportedValue<IFileActionService>();

                await service.MergeFiles(TEST_JSON_FILE_OBJECT, DATA_NODE_NAME, TEST_JSON_FILE_OBJECT_ITEMS_FOLDER);

                Assert.IsTrue(File.Exists(TEST_JSON_FILE_OBJECT_NEW));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile(TEST_JSON_FILE_OBJECT_NEW);
                var node = documentSrv.GetNode(jsonObj, DATA_NODE_NAME);

                Assert.AreEqual(2, (node as JObject).Properties().Count());

                var bigeggNode = (node as JObject).Properties().First(n => n.Name == "BigEgg").Value as JObject;
                Assert.IsNotNull(bigeggNode);
                Assert.AreEqual("BigEgg", bigeggNode.Property("name").Value);
                Assert.AreEqual(30L, bigeggNode.Property("age").Value);
                Assert.AreEqual("Male", bigeggNode.Property("gender").Value);

                var pupilNode = (node as JObject).Properties().First(n => n.Name == "Pupil").Value as JObject;
                Assert.IsNotNull(pupilNode);
                Assert.AreEqual("Pupil", pupilNode.Property("name").Value);
                Assert.AreEqual(31L, pupilNode.Property("age").Value);
                Assert.AreEqual("Female", pupilNode.Property("gender").Value);
            }

            [TestMethod]
            public async Task MergeObjectInArray_NotEmpty()
            {
                var service = Container.GetExportedValue<IFileActionService>();

                await service.MergeFiles(TEST_JSON_FILE_ARRAY, NUMBER_NODE_NAME, TEST_JSON_FILE_ARRAY_ITEMS_FOLDER);

                Assert.IsTrue(File.Exists(TEST_JSON_FILE_ARRAY_NEW));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile(TEST_JSON_FILE_ARRAY_NEW);
                var node = documentSrv.GetNode(jsonObj, NUMBER_NODE_NAME);

                Assert.AreEqual(4, node.Children().Count());

                var bigeggNode = node.Children().ToList()[2] as JObject;
                Assert.IsNotNull(bigeggNode);
                Assert.AreEqual("BigEgg", bigeggNode.Property("name").Value);
                Assert.AreEqual(30L, bigeggNode.Property("age").Value);
                Assert.AreEqual("Male", bigeggNode.Property("gender").Value);

                var pupilNode = node.Children().ToList()[3] as JObject;
                Assert.IsNotNull(pupilNode);
                Assert.AreEqual("Pupil", pupilNode.Property("name").Value);
                Assert.AreEqual(31L, pupilNode.Property("age").Value);
                Assert.AreEqual("Female", pupilNode.Property("gender").Value);
            }

            [TestMethod]
            public async Task MergeObjectInObject_NotEmpty()
            {
                var service = Container.GetExportedValue<IFileActionService>();

                await service.MergeFiles(TEST_JSON_FILE_OBJECT, STATUS_NODE_NAME, TEST_JSON_FILE_OBJECT_ITEMS_FOLDER);

                Assert.IsTrue(File.Exists(TEST_JSON_FILE_OBJECT_NEW));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile(TEST_JSON_FILE_OBJECT_NEW);
                var node = documentSrv.GetNode(jsonObj, STATUS_NODE_NAME);

                Assert.AreEqual(4, (node as JObject).Properties().Count());

                var bigeggNode = (node as JObject).Properties().First(n => n.Name == "BigEgg").Value as JObject;
                Assert.IsNotNull(bigeggNode);
                Assert.AreEqual("BigEgg", bigeggNode.Property("name").Value);
                Assert.AreEqual(30L, bigeggNode.Property("age").Value);
                Assert.AreEqual("Male", bigeggNode.Property("gender").Value);

                var pupilNode = (node as JObject).Properties().First(n => n.Name == "Pupil").Value as JObject;
                Assert.IsNotNull(pupilNode);
                Assert.AreEqual("Pupil", pupilNode.Property("name").Value);
                Assert.AreEqual(31L, pupilNode.Property("age").Value);
                Assert.AreEqual("Female", pupilNode.Property("gender").Value);
            }

            [TestMethod]
            public async Task MergeToValue()
            {
                var service = Container.GetExportedValue<IFileActionService>();

                await AssertHelper.ExpectedExceptionAsync<NotSupportedException>(() => service.MergeFiles(TEST_JSON_FILE_OBJECT, VERSION_NODE_NAME, TEST_JSON_FILE_OBJECT_ITEMS_FOLDER));
            }

            [TestMethod]
            public async Task MergeObjectInArray_WithProgress()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                var calledCount_MergeObjectInArray = 0;
                var progress = new Progress<IProgressReport>(report =>
                {
                    Assert.AreEqual(2, report.Total);
                    Assert.AreEqual(calledCount_MergeObjectInArray++, report.Current);
                });
                await service.MergeFiles(TEST_JSON_FILE_ARRAY, ARRAY_NODE_NAME, TEST_JSON_FILE_ARRAY_ITEMS_FOLDER);

                Assert.IsTrue(File.Exists(TEST_JSON_FILE_ARRAY_NEW));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile(TEST_JSON_FILE_ARRAY_NEW);
                var node = documentSrv.GetNode(jsonObj, ARRAY_NODE_NAME);

                Assert.AreEqual(2, node.Children().Count());

                var bigeggNode = node.Children().First() as JObject;
                Assert.IsNotNull(bigeggNode);
                Assert.AreEqual("BigEgg", bigeggNode.Property("name").Value);
                Assert.AreEqual(30L, bigeggNode.Property("age").Value);
                Assert.AreEqual("Male", bigeggNode.Property("gender").Value);

                var pupilNode = node.Children().Last() as JObject;
                Assert.IsNotNull(pupilNode);
                Assert.AreEqual("Pupil", pupilNode.Property("name").Value);
                Assert.AreEqual(31L, pupilNode.Property("age").Value);
                Assert.AreEqual("Female", pupilNode.Property("gender").Value);
            }

            [TestMethod]
            public async Task MergeObjectInObject_WithProgress()
            {
                var service = Container.GetExportedValue<IFileActionService>();
                var calledCount_SplitObjectInObject = 0;
                var progress = new Progress<IProgressReport>(report =>
                {
                    Assert.AreEqual(2, report.Total);
                    Assert.AreEqual(calledCount_SplitObjectInObject++, report.Current);
                });
                await service.MergeFiles(TEST_JSON_FILE_OBJECT, DATA_NODE_NAME, TEST_JSON_FILE_OBJECT_ITEMS_FOLDER);

                Assert.IsTrue(File.Exists(TEST_JSON_FILE_OBJECT_NEW));

                var documentSrv = Container.GetExportedValue<IJsonDocumentService>();
                var jsonObj = documentSrv.ReadJsonFile(TEST_JSON_FILE_OBJECT_NEW);
                var node = documentSrv.GetNode(jsonObj, DATA_NODE_NAME);

                Assert.AreEqual(2, (node as JObject).Properties().Count());

                var bigeggNode = (node as JObject).Properties().First(n => n.Name == "BigEgg").Value as JObject;
                Assert.IsNotNull(bigeggNode);
                Assert.AreEqual("BigEgg", bigeggNode.Property("name").Value);
                Assert.AreEqual(30L, bigeggNode.Property("age").Value);
                Assert.AreEqual("Male", bigeggNode.Property("gender").Value);

                var pupilNode = (node as JObject).Properties().First(n => n.Name == "Pupil").Value as JObject;
                Assert.IsNotNull(pupilNode);
                Assert.AreEqual("Pupil", pupilNode.Property("name").Value);
                Assert.AreEqual(31L, pupilNode.Property("age").Value);
                Assert.AreEqual("Female", pupilNode.Property("gender").Value);
            }
        }
    }
}
