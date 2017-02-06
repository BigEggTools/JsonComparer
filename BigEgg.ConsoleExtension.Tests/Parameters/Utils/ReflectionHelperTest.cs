namespace BigEgg.ConsoleExtension.Tests.Parameters.Utils
{
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BigEgg.ConsoleExtension.Parameters.Utils;

    using BigEgg.ConsoleExtension.Tests.Parameters.FakeParameters;

    [TestClass]
    public class ReflectionHelperTest
    {
        [TestMethod]
        public void GetAssemblyTest()
        {
            var assembly = ReflectionHelper.GetAssembly();
            Assert.IsNotNull(assembly);
        }

        [TestMethod]
        public void GetAssemblyAttributeTest()
        {
            var attribute = ReflectionHelper.GetAssemblyAttribute<AssemblyProductAttribute>();
            Assert.IsNotNull(attribute);
            Assert.AreEqual("BigEgg.Tools", attribute.Product);
        }

        [TestMethod]
        public void GetCommandTest_NoCommand()
        {
            var command = ReflectionHelper.GetCommand(typeof(EmptyClass));
            Assert.IsNull(command);
        }

        [TestMethod]
        public void GetCommandTest_HasCommand()
        {
            var command = ReflectionHelper.GetCommand(typeof(GitClone));
            Assert.IsNotNull(command);
            Assert.AreEqual("Clone", command.Name);
        }
    }
}
