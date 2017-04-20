namespace BigEgg.Tools.JsonComparer.Reports.Excel
{
    internal interface IExcelReportDocumentType
    {
        ExcelReportDocuemnt New();

        void Save(ExcelReportDocuemnt document, string filename);
    }
}