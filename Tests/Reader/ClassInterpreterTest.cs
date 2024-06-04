using System.Reflection;
using NUnit.Framework;
using Tests.TestAssembly;
using WrapperGenerator.IR;
using WrapperGenerator.Reader;

namespace Tests.Reader
{
    [TestFixture]
    [TestOf(typeof(ClassInterpreter))]
    public class ClassInterpreterTest
    {

        [Test]
        public void GenerateTestForTestClass()
        {
            IRClass irClass = ClassInterpreter.InterpretType(typeof(TestClass).GetTypeInfo());
            Assert.AreEqual("TestClass", irClass.Name);
            Assert.AreEqual("WrapperTests.TestAssembly", irClass.Namespace);
            Assert.AreEqual(1, irClass.Methods.Count);
            
        }
    }
}