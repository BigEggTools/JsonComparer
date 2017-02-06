namespace BigEgg.ConsoleExtension.Tests.Parameters
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.ConsoleExtension.Parameters;

    [TestClass]
    public class ParserSettingsTest
    {
        [TestMethod]
        public void DisposeTest_NotDisposeHelpWriter()
        {
            using (var helpWriter = new MockStringWriter())
            {
                using (var settings = ParserSettings.Builder().WithDefault().HelpWriter(helpWriter).Build())
                {
                    Assert.IsFalse(helpWriter.Disposed);
                }
                Assert.IsFalse(helpWriter.Disposed);
            }
        }

        public class MockStringWriter : StringWriter
        {
            public MockStringWriter()
            {
                Disposed = false;
            }

            public bool Disposed { get; private set; }

            protected override void Dispose(bool disposing)
            {
                Disposed = true;
                base.Dispose(disposing);
            }
        }
    }
}
