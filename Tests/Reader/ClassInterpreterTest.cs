using System.Reflection;
using NUnit.Framework;
using Tests.TestAssembly;
using WrapperGenerator.Reader;

namespace Tests.Reader
{
    [TestFixture]
    [TestOf(typeof(ClassInterpreter))]
    public class ClassInterpreterTest
    {
        private IRTypeGraph typeGraph;

        [Test]
        public void GenerateTestForTestClass()
        {
            var irClass = ClassInterpreter.InterpretType(typeof(TestClass).GetTypeInfo());
            Assert.AreEqual("TestClass", irClass.Name);
            Assert.AreEqual("WrapperTests.TestAssembly", irClass.Namespace);
            Assert.AreEqual(1, irClass.Methods.Count);
        }

        [Test]
        public void GenerateTestForTestStruct()
        {
            var irClass = ClassInterpreter.InterpretType(typeof(TestStruct).GetTypeInfo());
            Assert.AreEqual("TestStruct", irClass.Name);
            Assert.AreEqual("WrapperTests.TestAssembly", irClass.Namespace);
            Assert.AreEqual(1, irClass.Methods.Count);
        }

        [Test]
        public void GenerateTestForCyclicDependency()
        {
            var cyclicDependencyB = new CyclicDependencyB();
            var cyclicDependencyA = new CyclicDependencyA();

            var irClassForA = ClassInterpreter.InterpretType(cyclicDependencyA.GetType().GetTypeInfo());
            Assert.AreEqual("CyclicDependencyA", irClassForA.Name);

            var irClassForB = ClassInterpreter.InterpretType(cyclicDependencyB.GetType().GetTypeInfo());
            Assert.AreEqual("CyclicDependencyB", irClassForB.Name);

            // Check IR Field values
            Assert.AreEqual(1, irClassForA.Fields.Count);
            Assert.AreEqual("foo", irClassForA.Fields[0].Name);
            Assert.AreEqual(FieldAttributes.Public, irClassForA.Fields[0].FieldAttributes);

            Assert.AreEqual(1, irClassForB.Fields.Count);
            Assert.AreEqual("foo", irClassForB.Fields[0].Name);
            Assert.AreEqual(FieldAttributes.Public, irClassForB.Fields[0].FieldAttributes);
        }
    }
}