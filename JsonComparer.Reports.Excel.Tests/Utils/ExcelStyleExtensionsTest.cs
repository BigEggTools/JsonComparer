namespace JsonComparer.Reports.Excel.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;
    using ClosedXML.Excel;

    using BigEgg.Tools.JsonComparer.Reports.Excel.Utils;
    using BigEgg.Tools.JsonComparer.Reports.Excel.Configurations;

    public class ExcelStyleExtensionsTest
    {
        [TestClass]
        public class SetStyleToCell
        {
            private Mock<IXLCell> mockCell = new Mock<IXLCell>();
            private Mock<IXLStyle> mockStyle = new Mock<IXLStyle>();
            private Mock<IXLFont> mockFont = new Mock<IXLFont>();
            private Mock<IXLFill> mockFill = new Mock<IXLFill>();
            private Mock<IXLAlignment> mockAlignment = new Mock<IXLAlignment>();
            private Mock<IXLBorder> mockBorder = new Mock<IXLBorder>();


            [TestInitialize]
            public void TestInitialize()
            {
                mockStyle.SetupGet(c => c.Font).Returns(mockFont.Object);
                mockStyle.SetupGet(c => c.Fill).Returns(mockFill.Object);
                mockStyle.SetupGet(c => c.Alignment).Returns(mockAlignment.Object);
                mockStyle.SetupGet(c => c.Border).Returns(mockBorder.Object);
                mockCell.SetupGet(c => c.Style).Returns(mockStyle.Object);
            }

            [TestCleanup]
            public void TestCleanup()
            {
                mockCell.Reset();
                mockStyle.Reset();
                mockFont.Reset();
                mockFill.Reset();
                mockAlignment.Reset();
                mockBorder.Reset();
            }

            [TestMethod]
            public void BoldSetting()
            {
                var config = new StyleConfig() { Bold = null };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockFont.Verify(f => f.SetBold(It.IsAny<bool>()), Times.Never);

                mockFont.Setup(c => c.SetBold(It.IsAny<bool>())).Returns(mockStyle.Object);

                config = new StyleConfig() { Bold = false };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockFont.Verify(f => f.SetBold(It.Is<bool>(x => x == false)), Times.Once);

                config = new StyleConfig() { Bold = true };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockFont.Verify(f => f.SetBold(It.Is<bool>(x => x == true)), Times.Once);
            }

            [TestMethod]
            public void FontColorSetting()
            {
                var config = new StyleConfig() { FontColor = null };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockFont.Verify(f => f.SetFontColor(It.IsAny<XLColor>()), Times.Never);

                mockFont.Setup(c => c.SetFontColor(It.IsAny<XLColor>())).Returns(mockStyle.Object);
                config = new StyleConfig() { FontColor = XLColor.DarkBlue };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockFont.Verify(f => f.SetFontColor(It.Is<XLColor>(x => x == XLColor.DarkBlue)), Times.Once);
            }

            [TestMethod]
            public void BackgroundColorSetting()
            {
                var config = new StyleConfig() { BackgroundColor = null };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockFill.Verify(f => f.SetBackgroundColor(It.IsAny<XLColor>()), Times.Never);

                mockFill.Setup(f => f.SetBackgroundColor(It.IsAny<XLColor>())).Returns(mockStyle.Object);
                config = new StyleConfig() { BackgroundColor = XLColor.DarkBlue };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockFill.Verify(f => f.SetBackgroundColor(It.Is<XLColor>(x => x == XLColor.DarkBlue)), Times.Once);
            }

            [TestMethod]
            public void HorizontalAlignmentSetting()
            {
                var config = new StyleConfig() { HorizontalAlignment = null };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockAlignment.Verify(a => a.SetHorizontal(It.IsAny<XLAlignmentHorizontalValues>()), Times.Never);

                mockAlignment.Setup(c => c.SetHorizontal(It.IsAny<XLAlignmentHorizontalValues>())).Returns(mockStyle.Object);
                config = new StyleConfig() { HorizontalAlignment = XLAlignmentHorizontalValues.Center };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockAlignment.Verify(a => a.SetHorizontal(It.Is<XLAlignmentHorizontalValues>(x => x == XLAlignmentHorizontalValues.Center)), Times.Once);
            }

            [TestMethod]
            public void OutsideBorderSetting()
            {
                var config = new StyleConfig() { OutsideBorder = null };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockBorder.Verify(b => b.SetOutsideBorder(It.IsAny<XLBorderStyleValues>()), Times.Never);

                mockBorder.Setup(c => c.SetOutsideBorder(It.IsAny<XLBorderStyleValues>())).Returns(mockStyle.Object);

                config = new StyleConfig() { OutsideBorder = false };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockBorder.Verify(b => b.SetOutsideBorder(It.Is<XLBorderStyleValues>(x => x == XLBorderStyleValues.None)), Times.Once);

                config = new StyleConfig() { OutsideBorder = true };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockBorder.Verify(b => b.SetOutsideBorder(It.Is<XLBorderStyleValues>(x => x == XLBorderStyleValues.Thick)), Times.Once);
            }

            [TestMethod]
            public void InsideBorderBorderSetting()
            {
                var config = new StyleConfig() { InsideBorder = null };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockBorder.Verify(b => b.SetInsideBorder(It.IsAny<XLBorderStyleValues>()), Times.Never);

                mockBorder.Setup(c => c.SetInsideBorder(It.IsAny<XLBorderStyleValues>())).Returns(mockStyle.Object);

                config = new StyleConfig() { InsideBorder = false };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockBorder.Verify(b => b.SetInsideBorder(It.Is<XLBorderStyleValues>(x => x == XLBorderStyleValues.None)), Times.Once);

                config = new StyleConfig() { InsideBorder = true };
                ExcelStyleExtensions.SetStyle(mockCell.Object, config);
                mockBorder.Verify(b => b.SetInsideBorder(It.Is<XLBorderStyleValues>(x => x == XLBorderStyleValues.Thin)), Times.Once);
            }
        }

        [TestClass]
        public class SetStyleToRange
        {
            private Mock<IXLRange> mockRange = new Mock<IXLRange>();
            private Mock<IXLStyle> mockStyle = new Mock<IXLStyle>();
            private Mock<IXLFont> mockFont = new Mock<IXLFont>();
            private Mock<IXLFill> mockFill = new Mock<IXLFill>();
            private Mock<IXLAlignment> mockAlignment = new Mock<IXLAlignment>();
            private Mock<IXLBorder> mockBorder = new Mock<IXLBorder>();


            [TestInitialize]
            public void TestInitialize()
            {
                mockStyle.SetupGet(c => c.Font).Returns(mockFont.Object);
                mockStyle.SetupGet(c => c.Fill).Returns(mockFill.Object);
                mockStyle.SetupGet(c => c.Alignment).Returns(mockAlignment.Object);
                mockStyle.SetupGet(c => c.Border).Returns(mockBorder.Object);
                mockRange.SetupGet(c => c.Style).Returns(mockStyle.Object);
            }

            [TestCleanup]
            public void TestCleanup()
            {
                mockRange.Reset();
                mockStyle.Reset();
                mockFont.Reset();
                mockFill.Reset();
                mockAlignment.Reset();
                mockBorder.Reset();
            }

            [TestMethod]
            public void BoldSetting()
            {
                var config = new StyleConfig() { Bold = null };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockFont.Verify(f => f.SetBold(It.IsAny<bool>()), Times.Never);

                mockFont.Setup(c => c.SetBold(It.IsAny<bool>())).Returns(mockStyle.Object);

                config = new StyleConfig() { Bold = false };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockFont.Verify(f => f.SetBold(It.Is<bool>(x => x == false)), Times.Once);

                config = new StyleConfig() { Bold = true };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockFont.Verify(f => f.SetBold(It.Is<bool>(x => x == true)), Times.Once);
            }

            [TestMethod]
            public void FontColorSetting()
            {
                var config = new StyleConfig() { FontColor = null };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockFont.Verify(f => f.SetFontColor(It.IsAny<XLColor>()), Times.Never);

                mockFont.Setup(c => c.SetFontColor(It.IsAny<XLColor>())).Returns(mockStyle.Object);
                config = new StyleConfig() { FontColor = XLColor.DarkBlue };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockFont.Verify(f => f.SetFontColor(It.Is<XLColor>(x => x == XLColor.DarkBlue)), Times.Once);
            }

            [TestMethod]
            public void BackgroundColorSetting()
            {
                var config = new StyleConfig() { BackgroundColor = null };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockFill.Verify(f => f.SetBackgroundColor(It.IsAny<XLColor>()), Times.Never);

                mockFill.Setup(f => f.SetBackgroundColor(It.IsAny<XLColor>())).Returns(mockStyle.Object);
                config = new StyleConfig() { BackgroundColor = XLColor.DarkBlue };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockFill.Verify(f => f.SetBackgroundColor(It.Is<XLColor>(x => x == XLColor.DarkBlue)), Times.Once);
            }

            [TestMethod]
            public void HorizontalAlignmentSetting()
            {
                var config = new StyleConfig() { HorizontalAlignment = null };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockAlignment.Verify(a => a.SetHorizontal(It.IsAny<XLAlignmentHorizontalValues>()), Times.Never);

                mockAlignment.Setup(c => c.SetHorizontal(It.IsAny<XLAlignmentHorizontalValues>())).Returns(mockStyle.Object);
                config = new StyleConfig() { HorizontalAlignment = XLAlignmentHorizontalValues.Center };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockAlignment.Verify(a => a.SetHorizontal(It.Is<XLAlignmentHorizontalValues>(x => x == XLAlignmentHorizontalValues.Center)), Times.Once);
            }

            [TestMethod]
            public void OutsideBorderSetting()
            {
                var config = new StyleConfig() { OutsideBorder = null };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockBorder.Verify(b => b.SetOutsideBorder(It.IsAny<XLBorderStyleValues>()), Times.Never);

                mockBorder.Setup(c => c.SetOutsideBorder(It.IsAny<XLBorderStyleValues>())).Returns(mockStyle.Object);

                config = new StyleConfig() { OutsideBorder = false };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockBorder.Verify(b => b.SetOutsideBorder(It.Is<XLBorderStyleValues>(x => x == XLBorderStyleValues.None)), Times.Once);

                config = new StyleConfig() { OutsideBorder = true };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockBorder.Verify(b => b.SetOutsideBorder(It.Is<XLBorderStyleValues>(x => x == XLBorderStyleValues.Thick)), Times.Once);
            }

            [TestMethod]
            public void InsideBorderBorderSetting()
            {
                var config = new StyleConfig() { InsideBorder = null };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockBorder.Verify(b => b.SetInsideBorder(It.IsAny<XLBorderStyleValues>()), Times.Never);

                mockBorder.Setup(c => c.SetInsideBorder(It.IsAny<XLBorderStyleValues>())).Returns(mockStyle.Object);

                config = new StyleConfig() { InsideBorder = false };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockBorder.Verify(b => b.SetInsideBorder(It.Is<XLBorderStyleValues>(x => x == XLBorderStyleValues.None)), Times.Once);

                config = new StyleConfig() { InsideBorder = true };
                ExcelStyleExtensions.SetStyle(mockRange.Object, config);
                mockBorder.Verify(b => b.SetInsideBorder(It.Is<XLBorderStyleValues>(x => x == XLBorderStyleValues.Thin)), Times.Once);
            }
        }
    }
}
