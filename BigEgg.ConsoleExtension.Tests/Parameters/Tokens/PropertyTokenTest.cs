namespace BigEgg.ConsoleExtension.Tests.Parameters.Tokens
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.ConsoleExtension.Parameters;
    using BigEgg.ConsoleExtension.Parameters.Tokens;

    [TestClass]
    public class PropertyTokenTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var token = new PropertyToken("repository", "https://github.com/BigEggTools/JsonComparer.git");
            Assert.AreEqual("repository", token.Name);
            Assert.AreEqual(TokenType.Property, token.TokenType);
            Assert.AreEqual("https://github.com/BigEggTools/JsonComparer.git", token.Value);
        }
    }
}
