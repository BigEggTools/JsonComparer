namespace JsonComparer.Reports.Excel.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;
    using BigEgg.UnitTesting;

    using BigEgg.Tools.JsonComparer.CompareData;
    using BigEgg.Tools.JsonComparer.Reports;
    using BigEgg.Tools.JsonComparer.Reports.Excel;

    public class ExcelReportServiceTest
    {
        [TestClass]
        public class GeneralTest : TestClassBase
        {
            [TestMethod]
            public void DepenedencyInjection()
            {
                var service = Container.GetExportedValue<IReportService>();
                Assert.IsNotNull(service);
            }

            [TestMethod]
            public void ServiceInfo()
            {
                var service = Container.GetExportedValue<IReportService>();
                Assert.AreEqual("Excel", service.Name);
                Assert.IsTrue(service.SupportSplitOutputFiles);
            }
        }

        [TestClass]
        public class PreconditionCheck : TestClassBase
        {
            [TestMethod]
            public async Task CompareFiles_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IReportService>();
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(null, "path1", "path2", "result", false));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>(), "path1", "path2", "result", false));
            }

            [TestMethod]
            public async Task Path1_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IReportService>();
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>() { NewCompareFile() }, null, "path2", "result", false));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>() { NewCompareFile() }, "", "path2", "result", false));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>() { NewCompareFile() }, "    ", "path2", "result", false));
            }

            [TestMethod]
            public async Task Path2_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IReportService>();
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>() { NewCompareFile() }, "path1", null, "result", false));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>() { NewCompareFile() }, "path1", "", "result", false));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>() { NewCompareFile() }, "path1", "    ", "result", false));
            }

            [TestMethod]
            public async Task Output_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IReportService>();
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>() { NewCompareFile() }, "path1", "path2", null, false));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>() { NewCompareFile() }, "path1", "path2", "", false));
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>() { NewCompareFile() }, "path1", "path2", "    ", false));
            }

            [TestMethod]
            public async Task Path1EqualsPath2_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IReportService>();
                await AssertHelper.ExpectedExceptionAsync<ArgumentException>(() => service.Output(new List<CompareFile>() { NewCompareFile() }, "path1", "path1", "result", false));
            }


            private CompareFile NewCompareFile()
            {
                return new CompareFile("test1",
                    new List<CompareItem>()
                    {
                        CompareItem.WithData1("property", new List<CompareValue>()
                        {
                            new CompareValue("name", "value")
                        })
                    });
            }
        }

        [TestClass]
        public class OutputTest : MockTestClassBase
        {
            private readonly string OUTPUT_FOLDER = "result";

            [TestCleanup]
            public void TestCleanup()
            {
                if (Directory.Exists(OUTPUT_FOLDER))
                {
                    Directory.Delete(OUTPUT_FOLDER);
                }

                MockExcelReportDocumentType.Reset();
            }

            [TestMethod]
            public void CreateOutputDirectoryTest()
            {
                Assert.IsFalse(Directory.Exists(OUTPUT_FOLDER));

                var service = MockContainer.GetExportedValue<IReportService>();
                service.Output(new List<CompareFile>() { NewCompareFile("File1") }, "path1", "path2", "result", false);

                Assert.IsTrue(Directory.Exists(OUTPUT_FOLDER));
            }


            private CompareFile NewCompareFile(string fileName)
            {
                return new CompareFile("fileName",
                    new List<CompareItem>()
                    {
                        CompareItem.WithData1("property", new List<CompareValue>()
                        {
                            new CompareValue("name", "value")
                        })
                    });
            }
        }


        public abstract class MockTestClassBase
        {
            private readonly Mock<IExcelReportDocumentType> mockExcelReportDocumentType = new Mock<IExcelReportDocumentType>();

            public MockTestClassBase()
            {

                AggregateCatalog catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new TypeCatalog(
                    typeof(ExcelReportService)
                ));
                MockContainer = new CompositionContainer(catalog);
                MockContainer.ComposeExportedValue(mockExcelReportDocumentType.Object);

                CompositionBatch batch = new CompositionBatch();
                batch.AddExportedValue(MockContainer);
                MockContainer.Compose(batch);
            }


            public CompositionContainer MockContainer { get; private set; }
            internal Mock<IExcelReportDocumentType> MockExcelReportDocumentType { get { return mockExcelReportDocumentType; } }
        }
    }
}
