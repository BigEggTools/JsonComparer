namespace BigEgg.Tools.JsonComparer.Reports.Excel
{
    internal interface IExcelReportDocumentType
    {
        ExcelReportDocuemnt New();

        ExcelReportDocuemnt Open(string filename);

        void Save(ExcelReportDocuemnt document, string filename);
    }
}