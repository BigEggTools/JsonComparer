namespace BigEgg.ConsoleExtension.Tests.Parameters.Tokens
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.ConsoleExtension.Parameters.Tokens;

    [TestClass]
    public class UnknownTokenTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var token = new UnknownToken("asdf");
            Assert.AreEqual("asdf", token.Name);
            Assert.AreEqual(TokenType.Unknown, token.TokenType);
            Assert.IsTrue(string.IsNullOrWhiteSpace(token.Value));
        }
    }
}
