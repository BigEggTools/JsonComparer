namespace BigEgg.ConsoleExtension.Tests.Parameters.Tokens
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.ConsoleExtension.Parameters.Tokens;
    using ConsoleExtension.Parameters;

    [TestClass]
    public class HelpTokenTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var token = new HelpToken();
            Assert.AreEqual(ParameterConstants.TOKEN_HELP_NAME, token.Name);
            Assert.AreEqual(TokenType.Help, token.TokenType);
            Assert.IsTrue(string.IsNullOrWhiteSpace(token.Value));
        }
    }
}
