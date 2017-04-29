namespace JsonComparer.Reports.Excel.Tests.Configurations
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.Tools.JsonComparer.Reports.Excel.Configurations;

    [TestClass]
    public class ExcelReportConfigurationDocumentTest
    {
        [TestMethod]
        public void DefatulConfig()
        {
            var config = new ExcelReportConfigurationDocument();

            Assert.AreEqual(1, config.SkipColumn);
            Assert.AreEqual(1, config.SkipRow);
            Assert.IsNotNull(config.Header1Style);
            Assert.IsNotNull(config.Header2Style);
            Assert.IsNotNull(config.NewItemStyle);
            Assert.IsNotNull(config.RemovedItemStyle);
            Assert.IsNotNull(config.DifferentItemStyle);
            Assert.IsNotNull(config.DifferentValueStyle);
            Assert.IsNotNull(config.BorderStyle);
        }
    }
}
