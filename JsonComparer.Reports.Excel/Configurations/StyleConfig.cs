namespace BigEgg.Tools.JsonComparer.Reports.Excel.Configurations
{
    using ClosedXML.Excel;

    internal class StyleConfig
    {
        public bool? Bold { get; internal set; }

        public XLColor FontColor { get; internal set; }

        public XLColor BackgroundColor { get; internal set; }

        public XLAlignmentHorizontalValues? HorizontalAlignment { get; internal set; }

        public bool? OutsideBorder { get; internal set; }

        public bool? InsideBorder { get; internal set; }
    }
}
