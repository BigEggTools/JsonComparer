namespace BigEgg.Tools.JsonComparer.Reports.Excel.Utils
{
    using ClosedXML.Excel;

    using BigEgg.Tools.JsonComparer.Reports.Excel.Configurations;

    internal static class ExcelStyleExtensions
    {
        public static IXLCell SetStyle(this IXLCell cell, StyleConfig styleConfig)
        {
            if (styleConfig.Bold.HasValue)
                cell.Style.Font.SetBold(styleConfig.Bold.Value);

            if (styleConfig.FontColor != null)
                cell.Style.Font.SetFontColor(styleConfig.FontColor);

            if (styleConfig.BackgroundColor != null)
                cell.Style.Fill.SetBackgroundColor(styleConfig.BackgroundColor);

            if (styleConfig.HorizontalAlignment.HasValue)
                cell.Style.Alignment.SetHorizontal(styleConfig.HorizontalAlignment.Value);

            if (styleConfig.OutsideBorder.HasValue)
                cell.Style.Border.SetOutsideBorder(styleConfig.OutsideBorder.Value ? XLBorderStyleValues.Thick : XLBorderStyleValues.None);

            if (styleConfig.InsideBorder.HasValue)
                cell.Style.Border.SetInsideBorder(styleConfig.InsideBorder.Value ? XLBorderStyleValues.Thin : XLBorderStyleValues.None);

            return cell;
        }

        public static IXLRange SetStyle(this IXLRange range, StyleConfig styleConfig)
        {
            if (styleConfig.Bold.HasValue)
                range.Style.Font.SetBold(styleConfig.Bold.Value);

            if (styleConfig.FontColor != null)
                range.Style.Font.SetFontColor(styleConfig.FontColor);

            if (styleConfig.BackgroundColor != null)
                range.Style.Fill.SetBackgroundColor(styleConfig.BackgroundColor);

            if (styleConfig.HorizontalAlignment.HasValue)
                range.Style.Alignment.SetHorizontal(styleConfig.HorizontalAlignment.Value);

            if (styleConfig.OutsideBorder.HasValue)
                range.Style.Border.SetOutsideBorder(styleConfig.OutsideBorder.Value ? XLBorderStyleValues.Thick : XLBorderStyleValues.None);

            if (styleConfig.InsideBorder.HasValue)
                range.Style.Border.SetInsideBorder(styleConfig.InsideBorder.Value ? XLBorderStyleValues.Thin : XLBorderStyleValues.None);

            return range;
        }
    }
}
