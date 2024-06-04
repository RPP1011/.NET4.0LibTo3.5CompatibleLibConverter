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
        
        [Test]
        public void GenerateTestForTestStruct()
        {
            IRClass irClass = ClassInterpreter.InterpretType(typeof(TestStruct).GetTypeInfo());
            Assert.AreEqual("TestStruct", irClass.Name);
            Assert.AreEqual("WrapperTests.TestAssembly", irClass.Namespace);
            Assert.AreEqual(1, irClass.Methods.Count);
        }

        [Test]
        public void GenerateTestForCyclicDependency()
        {
            CyclicDependencyB cyclicDependencyB = new CyclicDependencyB();
            CyclicDependencyA cyclicDependencyA = new CyclicDependencyA();
            
            IRClass irClassForA = ClassInterpreter.InterpretType(cyclicDependencyA.GetType().GetTypeInfo());
            Assert.AreEqual("CyclicDependencyA", irClassForA.Name);
            
            IRClass irClassForB = ClassInterpreter.InterpretType(cyclicDependencyB.GetType().GetTypeInfo());
            Assert.AreEqual("CyclicDependencyB", irClassForB.Name);
            
            // Check IR Field values
            Assert.AreEqual(1, irClassForA.Fields.Count);
            Assert.AreEqual("cyclicDependencyA", irClassForA.Fields[0].Name);
            Assert.AreEqual(FieldAttributes.Public, irClassForA.Fields[0].FieldAttributes);
            
            Assert.AreEqual(1, irClassForB.Fields.Count);
            Assert.AreEqual("cyclicDependencyB", irClassForB.Fields[0].Name);
            Assert.AreEqual(FieldAttributes.Public, irClassForB.Fields[0].FieldAttributes);        }
    }
}