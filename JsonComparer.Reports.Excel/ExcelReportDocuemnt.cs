namespace BigEgg.Tools.JsonComparer.Reports.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ClosedXML.Excel;

    using BigEgg.Tools.JsonComparer.CompareData;
    using BigEgg.Tools.JsonComparer.Reports.Excel.Configurations;
    using BigEgg.Tools.JsonComparer.Reports.Excel.Utils;

    internal class ExcelReportDocuemnt
    {
        private readonly XLWorkbook workbook;
        private const int HEADER_ROW_COUNT = 2;


        public ExcelReportDocuemnt() : this(new XLWorkbook())
        { }

        public ExcelReportDocuemnt(XLWorkbook workbook)
        {
            Preconditions.NotNull(workbook);

            this.workbook = workbook;
        }


        public XLWorkbook Workbook { get { return workbook; } }


        public void NewSheet(CompareFile compareData, string path1, string path2, ExcelReportConfigurationDocument config)
        {
            Preconditions.NotNull(compareData, "compareData");
            Preconditions.Check(compareData.CompareItems.Count > 0, "CompareData should not be empty.");
            Preconditions.NotNullOrWhiteSpace(path1, "path1");
            Preconditions.NotNullOrWhiteSpace(path2, "path2");
            Preconditions.Check(!path1.Equals(path2, StringComparison.OrdinalIgnoreCase), "Path1 should not be same as Path2");

            var columns = compareData.CompareItems.First().Data.Select(x => x.Key).ToList();
            var worksheet = workbook.Worksheets.Add(compareData.FileName);

            OutputHeader(worksheet, columns, path1, path2, config);
            FillData(worksheet, columns, compareData, config);
            UpdateWorksheetStyles(worksheet, compareData.CompareItems.Count, columns.Count, config);
        }

        private void OutputHeader(IXLWorksheet worksheet, IList<string> columns, string path1, string path2, ExcelReportConfigurationDocument config)
        {
            var cell = worksheet.Cell(config.SkipRow + 1, config.SkipColumn + 1);
            cell.SetStyle(config.Header1Style).Value = $"Field Name";
            worksheet.Range(config.SkipRow + 1, config.SkipColumn + 1, config.SkipRow + 2, config.SkipColumn + 1).Merge();

            cell = worksheet.Cell(config.SkipRow + 1, config.SkipColumn + 2);
            cell.SetStyle(config.Header1Style).Value = $"Data from {path1}";
            worksheet.Range(config.SkipRow + 1, config.SkipColumn + 2, config.SkipRow + 1, config.SkipColumn + 1 + columns.Count).Merge();

            cell = worksheet.Cell(config.SkipRow + 1, config.SkipColumn + 1 + columns.Count + 1);
            cell.SetStyle(config.Header1Style).Value = $"Data from {path2}";
            worksheet.Range(config.SkipRow + 1, config.SkipColumn + 1 + columns.Count + 1, config.SkipRow + 1, config.SkipColumn + 1 + columns.Count * 2).Merge();

            for (var index = 1; index <= columns.Count; index++)
            {
                cell = worksheet.Cell(config.SkipRow + 1, config.SkipColumn + 1 + index);
                cell.SetStyle(config.Header2Style).Value = columns[index - 1];

                cell = worksheet.Cell(config.SkipRow + 1, config.SkipColumn + 1 + columns.Count + index);
                cell.SetStyle(config.Header2Style).Value = columns[index - 1];
            }

            worksheet.Range(config.SkipRow + 1, config.SkipColumn + 1, config.SkipRow + 2, config.SkipColumn + 1).SetStyle(config.BorderStyle);
            worksheet.Range(config.SkipRow + 1, config.SkipColumn + 2, config.SkipRow + 2, config.SkipColumn + 1 + columns.Count).SetStyle(config.BorderStyle);
            worksheet.Range(config.SkipRow + 1, config.SkipColumn + 1 + columns.Count + 2, config.SkipRow + 1, config.SkipColumn + 1 + columns.Count * 2).SetStyle(config.BorderStyle);
        }

        private void FillData(IXLWorksheet worksheet, IList<string> columns, CompareFile compareData, ExcelReportConfigurationDocument config)
        {
            var rowId = config.SkipRow + HEADER_ROW_COUNT + 1;

            foreach (var item in compareData.CompareItems)
            {
                FillItem(worksheet, rowId++, columns, item, config);
            }

            worksheet.Range(config.SkipRow + HEADER_ROW_COUNT + 1, config.SkipColumn + 1, rowId - 1, config.SkipColumn + 1).SetStyle(config.BorderStyle);
            worksheet.Range(config.SkipRow + HEADER_ROW_COUNT + 1, config.SkipColumn + 2, rowId - 1, config.SkipColumn + 1 + columns.Count).SetStyle(config.BorderStyle);
            worksheet.Range(config.SkipRow + HEADER_ROW_COUNT + 1, config.SkipColumn + 1 + columns.Count + 2, rowId - 1, config.SkipColumn + 1 + columns.Count * 2).SetStyle(config.BorderStyle);
        }

        private void FillItem(IXLWorksheet worksheet, int rowId, IList<string> columns, CompareItem compareItems, ExcelReportConfigurationDocument config)
        {
            var cell = worksheet.Cell(rowId, config.SkipColumn + 1);
            cell.Value = compareItems.PropertyName;

            if (!compareItems.HasData1)
            {
                FillNewItem(worksheet, rowId, columns, compareItems.Data.ToDictionary(item => item.Key, item => item.Value.Item2), config);
                return;
            }

            if (!compareItems.HasData2)
            {
                FillNewItem(worksheet, rowId, columns, compareItems.Data.ToDictionary(item => item.Key, item => item.Value.Item1), config);
                return;
            }

            FillDifferentItem(worksheet, rowId, columns, compareItems.Data, config);
        }

        private void FillNewItem(IXLWorksheet worksheet, int rowId, IList<string> columns, IDictionary<string, CompareValue> values, ExcelReportConfigurationDocument config)
        {
            for (var index = 1; index <= columns.Count; index++)
            {
                var cell = worksheet.Cell(rowId, config.SkipColumn + 1 + columns.Count + index);
                cell.SetStyle(config.NewItemStyle).Value = values[columns[index - 1]].Value;

                cell = worksheet.Cell(rowId, config.SkipColumn + 1);
                cell.SetStyle(config.NewItemStyle);
            }
        }

        private void FillRemovedItem(IXLWorksheet worksheet, int rowId, IList<string> columns, IDictionary<string, CompareValue> values, ExcelReportConfigurationDocument config)
        {
            for (var index = 1; index <= columns.Count; index++)
            {
                var cell = worksheet.Cell(rowId, config.SkipColumn + 1 + index);
                cell.SetStyle(config.RemovedItemStyle).Value = values[columns[index - 1]].Value;

                cell = worksheet.Cell(rowId, config.SkipColumn + 1);
                cell.SetStyle(config.RemovedItemStyle);
            }
        }

        private void FillDifferentItem(IXLWorksheet worksheet, int rowId, IList<string> columns, IDictionary<string, Tuple<CompareValue, CompareValue>> values, ExcelReportConfigurationDocument config)
        {
            var different = values.Any(item => !item.Value.Item1.Value.Equals(item.Value.Item2.Value));

            for (var index = 1; index <= columns.Count; index++)
            {
                var value1 = values[columns[index - 1]].Item1.Value;
                var value2 = values[columns[index - 1]].Item2.Value;

                var cell = worksheet.Cell(rowId, config.SkipColumn + 1 + index);
                cell.Value = value1;
                if (different)
                {
                    cell.SetStyle(value1.Equals(value2) ? config.DifferentItemStyle : config.DifferentValueStyle);
                }


                cell = worksheet.Cell(rowId, config.SkipColumn + 1 + columns.Count + index);
                cell.Value = value2;
                if (different)
                {
                    cell.SetStyle(value1.Equals(value2) ? config.DifferentItemStyle : config.DifferentValueStyle);
                }

                cell = worksheet.Cell(rowId, config.SkipColumn + 1);
                if (different)
                {
                    cell.SetStyle(config.DifferentItemStyle);
                }
            }
        }

        private void UpdateWorksheetStyles(IXLWorksheet worksheet, int dataCount, int columnsCount, ExcelReportConfigurationDocument config)
        {
            worksheet.Range(config.SkipRow + HEADER_ROW_COUNT + 1, config.SkipColumn + 1, config.SkipRow + HEADER_ROW_COUNT + dataCount, config.SkipColumn + 1 + columnsCount * 2)
                     .CreateTable();

            worksheet.Columns(1, config.SkipColumn).Width = 4;
            worksheet.Columns(config.SkipColumn + 1, config.SkipColumn + 1 + columnsCount * 2).AdjustToContents();
        }
    }
}
