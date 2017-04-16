namespace BigEgg.Tools.JsonComparer.Reports.Excel.Configurations
{
    using ClosedXML.Excel;

    internal class ExcelReportConfigurationDocument
    {
        public int SkipColumn { get { return 1; } }

        public int SkipRow { get { return 1; } }

        public StyleConfig Header1Style
        {
            get
            {
                return new StyleConfig()
                {
                    Bold = true,
                    BackgroundColor = XLColor.CornflowerBlue,
                    HorizontalAlignment = XLAlignmentHorizontalValues.Center
                };
            }
        }

        public StyleConfig Header2Style
        {
            get
            {
                return new StyleConfig()
                {
                    Bold = true,
                    FontColor = XLColor.DarkBlue,
                    BackgroundColor = XLColor.PaleAqua,
                    HorizontalAlignment = XLAlignmentHorizontalValues.Center
                };
            }
        }

        public StyleConfig NewItemStyle
        {
            get
            {
                return new StyleConfig()
                {
                    FontColor = XLColor.DarkGreen,
                    BackgroundColor = XLColor.LightGreen,
                };
            }
        }

        public StyleConfig RemovedItemStyle
        {
            get
            {
                return new StyleConfig()
                {
                    FontColor = XLColor.DarkRed,
                    BackgroundColor = XLColor.Pink,
                };
            }
        }

        public StyleConfig DifferentItemStyle
        {
            get
            {
                return new StyleConfig()
                {
                    BackgroundColor = XLColor.IndianYellow,
                };
            }
        }

        public StyleConfig DifferentValueStyle
        {
            get
            {
                return new StyleConfig()
                {
                    Bold = true,
                    FontColor = XLColor.Yellow,
                    BackgroundColor = XLColor.IndianYellow,
                };
            }
        }

        public StyleConfig BorderStyle
        {
            get
            {
                return new StyleConfig()
                {
                    OutsideBorder = true,
                    InsideBorder = true,
                };
            }
        }
    }
}
