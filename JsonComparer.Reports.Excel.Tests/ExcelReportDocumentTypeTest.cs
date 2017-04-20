using BigEgg.Tools.JsonComparer.CompareData;
using BigEgg.Tools.JsonComparer.Reports.Excel;
using BigEgg.Tools.JsonComparer.Reports.Excel.Configurations;
using BigEgg.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonComparer.Reports.Excel.Tests
{
    public class ExcelReportDocumentTypeTest : TestClassBase
    {
        [TestClass]
        public class GeneralTest : TestClassBase
        {
            [TestMethod]
            public void DepenedencyInjection()
            {
                var service = Container.GetExportedValue<IExcelReportDocumentType>();
                Assert.IsNotNull(service);
            }
        }

        [TestClass]
        public class NewTest : TestClassBase
        {
            [TestMethod]
            public void NewDocument()
            {
                var service = Container.GetExportedValue<IExcelReportDocumentType>();
                var document = service.New();
                Assert.IsNotNull(document);
            }
        }

        [TestClass]
        public class SaveTest : TestClassBase
        {
            private const string EXCEL_REPORT_FILE_NAME = "Test/test.xlsx";

            protected override void OnTestCleanup()
            {
                if (File.Exists(EXCEL_REPORT_FILE_NAME))
                {
                    File.Delete(EXCEL_REPORT_FILE_NAME);
                }
            }

            [TestMethod]
            public void Document_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IExcelReportDocumentType>();
                AssertHelper.ExpectedException<ArgumentNullException>(() => service.Save(null, "test"));
            }

            [TestMethod]
            public void FileName_PreconditionCheck()
            {
                var service = Container.GetExportedValue<IExcelReportDocumentType>();
                AssertHelper.ExpectedException<ArgumentException>(() => service.Save(new ExcelReportDocuemnt(), null));
                AssertHelper.ExpectedException<ArgumentException>(() => service.Save(new ExcelReportDocuemnt(), ""));
                AssertHelper.ExpectedException<ArgumentException>(() => service.Save(new ExcelReportDocuemnt(), "    "));
            }

            [TestMethod]
            public void File_NotExist()
            {
                var service = Container.GetExportedValue<IExcelReportDocumentType>();
                var document = service.New();
                document.NewSheet(
                    new CompareFile("test1",
                        new List<CompareItem>()
                        {
                            CompareItem.WithData1("property", new List<CompareValue>()
                            {
                                new CompareValue("name", "value")
                            })
                        }),
                    "path1", "path2", new ExcelReportConfigurationDocument());

                Assert.IsFalse(File.Exists(EXCEL_REPORT_FILE_NAME));
                service.Save(document, EXCEL_REPORT_FILE_NAME);
                Assert.IsTrue(File.Exists(EXCEL_REPORT_FILE_NAME));
            }

            [TestMethod]
            public void File_Exist()
            {
                var service = Container.GetExportedValue<IExcelReportDocumentType>();
                var document = service.New();
                document.NewSheet(
                    new CompareFile("test1",
                        new List<CompareItem>()
                        {
                            CompareItem.WithData1("property", new List<CompareValue>()
                            {
                                new CompareValue("name", "value")
                            })
                        }),
                    "path1", "path2", new ExcelReportConfigurationDocument());

                service.Save(document, EXCEL_REPORT_FILE_NAME);

                Assert.IsTrue(File.Exists(EXCEL_REPORT_FILE_NAME));
                service.Save(document, EXCEL_REPORT_FILE_NAME);
                Assert.IsTrue(File.Exists(EXCEL_REPORT_FILE_NAME));
            }
        }
    }
}
