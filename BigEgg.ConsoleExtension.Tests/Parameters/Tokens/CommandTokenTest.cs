namespace BigEgg.ConsoleExtension.Tests.Parameters.Tokens
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.ConsoleExtension.Parameters;
    using BigEgg.ConsoleExtension.Parameters.Tokens;

    [TestClass]
    public class CommandTokenTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var token = new CommandToken("clone");
            Assert.AreEqual(ParameterConstants.TOKEN_COMMAMD_NAME, token.Name);
            Assert.AreEqual(TokenType.Command, token.TokenType);
            Assert.AreEqual("clone", token.Value);
        }
    }
}
