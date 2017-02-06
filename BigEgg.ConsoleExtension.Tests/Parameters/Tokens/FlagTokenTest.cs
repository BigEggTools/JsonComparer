namespace BigEgg.ConsoleExtension.Tests.Parameters.Tokens
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.ConsoleExtension.Parameters.Tokens;

    [TestClass]
    public class FlagTokenTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var token = new FlagToken("recursive");
            Assert.AreEqual("recursive", token.Name);
            Assert.AreEqual(TokenType.Flag, token.TokenType);
            Assert.IsTrue(string.IsNullOrWhiteSpace(token.Value));
        }
    }
}
