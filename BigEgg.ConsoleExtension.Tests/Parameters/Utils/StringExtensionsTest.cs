namespace BigEgg.ConsoleExtension.Tests.Parameters.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BigEgg.ConsoleExtension.Parameters.Utils;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void JoinNewLineTest()
        {
            var builder1 = new StringBuilder();
            builder1.AppendLine("  asdf1234");
            builder1.Append("asdf1234");

            var builder2 = new StringBuilder();

            var builder3 = new StringBuilder();
            builder3.AppendLine("  asdf1234");

            var builderArray = new List<StringBuilder>() { builder1, builder2, builder3 };
            var result = builderArray.JoinNewLine();

            var expect = $"  asdf1234{Environment.NewLine}asdf1234{Environment.NewLine}{Environment.NewLine}  asdf1234{Environment.NewLine}";
            Assert.AreEqual(expect, result);
        }
    }
}
