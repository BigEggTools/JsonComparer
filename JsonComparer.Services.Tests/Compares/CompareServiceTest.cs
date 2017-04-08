using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using BigEgg.UnitTesting;

using BigEgg.Tools.JsonComparer.Services.Compares;
using BigEgg.Tools.JsonComparer.Services.Compares.Configurations;
using BigEgg.Progress;
using System.Threading.Tasks;
using BigEgg.Tools.JsonComparer.JsonDocuments;
using System.Collections.Generic;
using BigEgg.Tools.JsonComparer.CompareData;

namespace BigEgg.Tools.JsonComparer.Services.Tests.Compares
{
    public class CompareServiceTest
    {
        [TestClass]
        public class GeneralTest : TestClassBase
        {
            [TestMethod]
            public void InjectionTest()
            {
                var service = Container.GetExportedValue<ICompareService>();

                Assert.IsNotNull(service);
            }
        }

        [TestClass]
        public class ContractTest : MockTestClassBase
        {
            [TestMethod]
            public async Task Path1_PreconditionCheck()
            {
                var service = MockContainer.GetExportedValue<ICompareService>();

                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare(null, "path2", "configFile"));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("", "path2", "configFile"));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("   ", "path2", "configFile"));
            }

            [TestMethod]
            public async Task Path2_PreconditionCheck()
            {
                var service = MockContainer.GetExportedValue<ICompareService>();

                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("path1", null, "configFile"));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("path1", "", "configFile"));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("path1", "    ", "configFile"));
            }

            [TestMethod]
            public async Task Path1EqualPath2_PreconditionCheck()
            {
                var service = MockContainer.GetExportedValue<ICompareService>();

                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("path1", "path1", "configFile"));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("path1", "Path1", "configFile"));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("Path1", "path1", "configFile"));
            }

            [TestMethod]
            public async Task ConfigFile_PreconditionCheck()
            {
                var service = MockContainer.GetExportedValue<ICompareService>();

                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("path1", "path2", null));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("path1", "path2", ""));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Compare("path1", "path2", "    "));
            }
        }

        [TestClass]
        public class LogicTest : MockTestClassBase
        {
            private const string COMPAREPATH_SAME = "TestData\\Compares\\ComparePath\\SameFiles";
            private const string COMPAREPATH_PATH1LESS = "TestData\\Compares\\ComparePath\\Path1Less";
            private const string COMPAREPATH_PATH2LESS = "TestData\\Compares\\ComparePath\\Path2Less";
            private const string COMPAREPATH_DIFFERENTFILES = "TestData\\Compares\\ComparePath\\DifferentFiles";
            private const string PATH1 = "path1";
            private const string PATH2 = "path2";


            [TestCleanup]
            public void TestCleanup()
            {
                MockCompareConfigDocumentType.Reset();
                MockReadFileService.Reset();
                MockAnalyzeService.Reset();
            }

            [TestMethod]
            public async Task ConfigFileNotExist()
            {
                var service = MockContainer.GetExportedValue<ICompareService>();
                MockCompareConfigDocumentType.Setup(x => x.Read(It.IsAny<string>())).Throws<FileNotFoundException>();

                await AssertHelper.ExpectedExceptionAsync<FileNotFoundException>(() => service.Compare("path1", "path2", "configFile"));
            }

            [TestMethod]
            public async Task SameFileTest()
            {
                var service = MockContainer.GetExportedValue<ICompareService>();
                MockCompareConfigDocumentType.Setup(x => x.Read(It.IsAny<string>())).Returns(new CompareConfigDocument());
                MockReadFileService.Setup(x => x.GetJsonDocument(It.IsAny<string>(), It.IsAny<CompareConfigDocument>()))
                                   .Callback((string path, CompareConfigDocument config) =>
                                   {
                                       Assert.IsTrue(path.StartsWith(Path.Combine(COMPAREPATH_SAME, PATH1))
                                                  || path.StartsWith(Path.Combine(COMPAREPATH_SAME, PATH2)));
                                       Assert.IsNotNull(config);
                                   })
                                   .ReturnsAsync((string path, CompareConfigDocument config) => new JsonDocument(path, new List<KeyValuePair<string, FieldType>>()));
                MockAnalyzeService.Setup(x => x.Compare(It.IsAny<JsonDocument>(), It.IsAny<JsonDocument>()))
                                  .Callback((JsonDocument document1, JsonDocument document2) =>
                                  {
                                      Assert.IsNotNull(document1);
                                      Assert.IsNotNull(document2);
                                  })
                                  .Returns((JsonDocument document1, JsonDocument document2) => new CompareFile("article", new List<CompareItem>()));

                var calledCount_SplitObjectInObject = 0;
                var progress = new Progress<IProgressReport>(report =>
                {
                    Assert.AreEqual(1, report.Total);
                    Assert.AreEqual(calledCount_SplitObjectInObject++, report.Current);
                });

                var result = await service.Compare(Path.Combine(COMPAREPATH_SAME, PATH1), Path.Combine(COMPAREPATH_SAME, PATH2), "configFile");
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("article", result[0].FileName);
            }

            [TestMethod]
            public async Task Path1LessTest()
            {
                var service = MockContainer.GetExportedValue<ICompareService>();
                MockCompareConfigDocumentType.Setup(x => x.Read(It.IsAny<string>())).Returns(new CompareConfigDocument());
                MockReadFileService.Setup(x => x.GetJsonDocument(It.IsAny<string>(), It.IsAny<CompareConfigDocument>()))
                                   .Callback((string path, CompareConfigDocument config) =>
                                   {
                                       Assert.IsTrue(path.StartsWith(Path.Combine(COMPAREPATH_PATH1LESS, PATH1))
                                                  || path.StartsWith(Path.Combine(COMPAREPATH_PATH1LESS, PATH2)));
                                       Assert.IsNotNull(config);
                                   })
                                   .ReturnsAsync((string path, CompareConfigDocument config) =>
                                   {
                                       return path.StartsWith(Path.Combine(COMPAREPATH_PATH1LESS, PATH1))
                                               ? null
                                               : new JsonDocument(path, new List<KeyValuePair<string, FieldType>>());
                                   });
                MockAnalyzeService.Setup(x => x.Compare(It.IsAny<JsonDocument>(), It.IsAny<JsonDocument>()))
                                  .Callback((JsonDocument document1, JsonDocument document2) =>
                                  {
                                      Assert.IsNull(document1);
                                      Assert.IsNotNull(document2);
                                  })
                                  .Returns((JsonDocument document1, JsonDocument document2) => new CompareFile("article", new List<CompareItem>()));

                var calledCount_SplitObjectInObject = 0;
                var progress = new Progress<IProgressReport>(report =>
                {
                    Assert.AreEqual(1, report.Total);
                    Assert.AreEqual(calledCount_SplitObjectInObject++, report.Current);
                });

                var result = await service.Compare(Path.Combine(COMPAREPATH_PATH1LESS, PATH1), Path.Combine(COMPAREPATH_PATH1LESS, PATH2), "configFile");
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("article", result[0].FileName);
            }

            [TestMethod]
            public async Task Path2LessTest()
            {
                var service = MockContainer.GetExportedValue<ICompareService>();
                MockCompareConfigDocumentType.Setup(x => x.Read(It.IsAny<string>())).Returns(new CompareConfigDocument());
                MockReadFileService.Setup(x => x.GetJsonDocument(It.IsAny<string>(), It.IsAny<CompareConfigDocument>()))
                                   .Callback((string path, CompareConfigDocument config) =>
                                   {
                                       Assert.IsTrue(path.StartsWith(Path.Combine(COMPAREPATH_PATH2LESS, PATH1))
                                                  || path.StartsWith(Path.Combine(COMPAREPATH_PATH2LESS, PATH2)));
                                       Assert.IsNotNull(config);
                                   })
                                   .ReturnsAsync((string path, CompareConfigDocument config) =>
                                   {
                                       return path.StartsWith(Path.Combine(COMPAREPATH_PATH2LESS, PATH2))
                                               ? null
                                               : new JsonDocument(path, new List<KeyValuePair<string, FieldType>>());
                                   });
                MockAnalyzeService.Setup(x => x.Compare(It.IsAny<JsonDocument>(), It.IsAny<JsonDocument>()))
                                  .Callback((JsonDocument document1, JsonDocument document2) =>
                                  {
                                      Assert.IsNotNull(document1);
                                      Assert.IsNull(document2);
                                  })
                                  .Returns((JsonDocument document1, JsonDocument document2) => new CompareFile("article", new List<CompareItem>()));

                var calledCount_SplitObjectInObject = 0;
                var progress = new Progress<IProgressReport>(report =>
                {
                    Assert.AreEqual(1, report.Total);
                    Assert.AreEqual(calledCount_SplitObjectInObject++, report.Current);
                });

                var result = await service.Compare(Path.Combine(COMPAREPATH_PATH2LESS, PATH1), Path.Combine(COMPAREPATH_PATH2LESS, PATH2), "configFile");
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("article", result[0].FileName);
            }

            [TestMethod]
            public async Task DifferentFilesTest()
            {
                var service = MockContainer.GetExportedValue<ICompareService>();
                MockCompareConfigDocumentType.Setup(x => x.Read(It.IsAny<string>())).Returns(new CompareConfigDocument());
                MockReadFileService.Setup(x => x.GetJsonDocument(It.IsAny<string>(), It.IsAny<CompareConfigDocument>()))
                                   .Callback((string path, CompareConfigDocument config) =>
                                   {
                                       Assert.IsTrue(path.StartsWith(Path.Combine(COMPAREPATH_DIFFERENTFILES, PATH1))
                                                  || path.StartsWith(Path.Combine(COMPAREPATH_DIFFERENTFILES, PATH2)));
                                       Assert.IsNotNull(config);
                                   })
                                   .ReturnsAsync((string path, CompareConfigDocument config) =>
                                   {
                                       if (path.EndsWith("article1.json"))
                                       {
                                           return path.StartsWith(Path.Combine(COMPAREPATH_DIFFERENTFILES, PATH2))
                                                   ? null
                                                   : new JsonDocument(path, new List<KeyValuePair<string, FieldType>>());
                                       }
                                       else
                                       {
                                           return path.StartsWith(Path.Combine(COMPAREPATH_DIFFERENTFILES, PATH1))
                                                   ? null
                                                   : new JsonDocument(path, new List<KeyValuePair<string, FieldType>>());
                                       }
                                   });
                MockAnalyzeService.Setup(x => x.Compare(It.IsAny<JsonDocument>(), It.IsAny<JsonDocument>()))
                                  .Callback((JsonDocument document1, JsonDocument document2) =>
                                  {
                                      Assert.IsTrue(document1 == null || document2 == null);
                                  })
                                  .Returns((JsonDocument document1, JsonDocument document2) => new CompareFile(document1 == null ? "article2" : "article1", new List<CompareItem>()));

                var calledCount_SplitObjectInObject = 0;
                var progress = new Progress<IProgressReport>(report =>
                {
                    Assert.AreEqual(2, report.Total);
                    Assert.AreEqual(calledCount_SplitObjectInObject++, report.Current);
                });

                var result = await service.Compare(Path.Combine(COMPAREPATH_DIFFERENTFILES, PATH1), Path.Combine(COMPAREPATH_DIFFERENTFILES, PATH2), "configFile");
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count);
                Assert.AreEqual("article1", result[0].FileName);
                Assert.AreEqual("article2", result[1].FileName);
            }
        }

        public abstract class MockTestClassBase
        {
            private readonly Mock<ICompareConfigDocumentType> mockCompareConfigDocumentType = new Mock<ICompareConfigDocumentType>();
            private readonly Mock<IReadFileService> mockReadFileService = new Mock<IReadFileService>();
            private readonly Mock<IAnalyzeService> mockAnalyzeService = new Mock<IAnalyzeService>();

            public MockTestClassBase()
            {

                AggregateCatalog catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new TypeCatalog(
                    typeof(CompareService)
                ));
                MockContainer = new CompositionContainer(catalog);
                MockContainer.ComposeExportedValue(mockCompareConfigDocumentType.Object);
                MockContainer.ComposeExportedValue(mockReadFileService.Object);
                MockContainer.ComposeExportedValue(mockAnalyzeService.Object);

                CompositionBatch batch = new CompositionBatch();
                batch.AddExportedValue(MockContainer);
                MockContainer.Compose(batch);
            }


            public CompositionContainer MockContainer { get; private set; }
            internal Mock<ICompareConfigDocumentType> MockCompareConfigDocumentType { get { return mockCompareConfigDocumentType; } }
            internal Mock<IReadFileService> MockReadFileService { get { return mockReadFileService; } }
            internal Mock<IAnalyzeService> MockAnalyzeService { get { return mockAnalyzeService; } }
        }
    }
}
