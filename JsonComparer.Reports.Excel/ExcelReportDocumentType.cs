namespace BigEgg.Tools.JsonComparer.Reports.Excel
{
    using System.ComponentModel.Composition;
    using System.IO;

    [Export(typeof(IExcelReportDocumentType))]
    internal class ExcelReportDocumentType : IExcelReportDocumentType
    {
        public ExcelReportDocuemnt New()
        {
            return new ExcelReportDocuemnt();
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
