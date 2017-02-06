namespace BigEgg.ConsoleExtension.Tests.Parameters.Output
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.ConsoleExtension.Parameters.Output;
    using BigEgg.ConsoleExtension.Parameters.Results;
    using BigEgg.ConsoleExtension.Parameters.Errors;

    [TestClass]
    public class TextBuilderTest
    {
        [TestMethod]
        public void BuildTest_VersionRequest()
        {
            var parseResult = new ParseFailedResult(new List<Error>() { new VersionRequestError() });
            var output = TextBuilder.Build(parseResult);
            var lines = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            Assert.AreEqual(6, lines.Length);
        }
    }
}
