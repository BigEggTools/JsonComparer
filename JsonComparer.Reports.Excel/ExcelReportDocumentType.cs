namespace BigEgg.Tools.JsonComparer.Reports.Excel
{
    using System.ComponentModel.Composition;
    using System.IO;

    using ClosedXML.Excel;

    [Export]
    internal class ExcelReportDocumentType : IExcelReportDocumentType
    {
        public ExcelReportDocuemnt New()
        {
            return new ExcelReportDocuemnt();
        }

        public ExcelReportDocuemnt Open(string filename)
        {
            Preconditions.NotNullOrWhiteSpace(filename, "filename");
            Preconditions.Check<FileNotFoundException>(File.Exists(filename));

            return new ExcelReportDocuemnt(new XLWorkbook(filename));
        }

        public void Save(ExcelReportDocuemnt document, string filename)
        {
            Preconditions.NotNull(document, "document");
            Preconditions.NotNullOrWhiteSpace(filename, "filename");

            if (File.Exists(filename)) { File.Delete(filename); }

            document.Workbook.SaveAs(filename);
        }
    }
}
