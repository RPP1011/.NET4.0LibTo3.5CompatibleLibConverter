using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;
using Tests.TestAssembly;
using WrapperGenerator.Reader;

namespace Tests.Reader
{
    [TestFixture]
    [TestOf(typeof(MethodInterpreter))]
    public class MethodInterpreterTest
    {
        private IRTypeGraph typeGraph;
        
        [SetUp]
        public void Setup()
        {
            typeGraph = new IRTypeGraph();
        }
        
        [Test]
        public void InterpretMethod_PublicMethod()
        {
            var methodInfo = typeof(MethodTestClass).GetMethod("PublicMethod");
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("PublicMethod", irMethod.Name);
            Assert.AreEqual(typeof(void), irMethod.ReturnType);
            Assert.IsEmpty(irMethod.Parameters);
        }

        [Test]
        public void InterpretMethod_PrivateMethod()
        {
            var methodInfo =
                typeof(MethodTestClass).GetMethod("PrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance);
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("PrivateMethod", irMethod.Name);
            Assert.AreEqual(typeof(void), irMethod.ReturnType);
            Assert.IsEmpty(irMethod.Parameters);
        }

        [Test]
        public void InterpretMethod_VoidMethod()
        {
            var methodInfo = typeof(MethodTestClass).GetMethod("VoidMethod");
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("VoidMethod", irMethod.Name);
            Assert.AreEqual(typeof(void), irMethod.ReturnType);
            Assert.IsEmpty(irMethod.Parameters);
        }

        [Test]
        public void InterpretMethod_MethodWithParameters()
        {
            var methodInfo = typeof(MethodTestClass).GetMethod("MethodWithParameters");
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("MethodWithParameters", irMethod.Name);
            Assert.AreEqual(typeof(void), irMethod.ReturnType);
            Assert.AreEqual(2, irMethod.Parameters.Count);
            Assert.AreEqual("param1", irMethod.Parameters[0].Name);
            Assert.AreEqual(typeof(int), irMethod.Parameters[0].Type);
            Assert.AreEqual("param2", irMethod.Parameters[1].Name);
            Assert.AreEqual(typeof(string), irMethod.Parameters[1].Type);
        }

        [Test]
        public void InterpretMethod_MethodWithReturnType()
        {
            var methodInfo = typeof(MethodTestClass).GetMethod("MethodWithReturnType");
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("MethodWithReturnType", irMethod.Name);
            Assert.AreEqual(typeof(int), irMethod.ReturnType);
            Assert.IsEmpty(irMethod.Parameters);
        }

        [Test]
        public void InterpretMethod_MethodWithParametersAndReturnType()
        {
            var methodInfo = typeof(MethodTestClass).GetMethod("MethodWithParametersAndReturnType");
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("MethodWithParametersAndReturnType", irMethod.Name);
            Assert.AreEqual(typeof(string), irMethod.ReturnType);
            Assert.AreEqual(2, irMethod.Parameters.Count);
            Assert.AreEqual("param1", irMethod.Parameters[0].Name);
            Assert.AreEqual(typeof(int), irMethod.Parameters[0].Type);
            Assert.AreEqual("param2", irMethod.Parameters[1].Name);
            Assert.AreEqual(typeof(string), irMethod.Parameters[1].Type);
        }

        [Test]
        public void InterpretMethod_MethodWithGenericParameters()
        {
            var methodInfo = typeof(MethodTestClass).GetMethod("MethodWithGenericParameters");
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("MethodWithGenericParameters", irMethod.Name);
            Assert.AreEqual(typeof(void), irMethod.ReturnType);
            Assert.AreEqual(1, irMethod.Parameters.Count);
            Assert.AreEqual("param", irMethod.Parameters[0].Name);
            Assert.IsTrue(irMethod.Parameters[0].Type.BackingType.IsGenericParameter);
        }

        [Test]
        public void InterpretMethod_MethodWithGenericReturnType()
        {
            var methodInfo = typeof(MethodTestClass).GetMethod("MethodWithGenericReturnType");
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("MethodWithGenericReturnType", irMethod.Name);
            Assert.IsTrue(irMethod.ReturnType.BackingType.IsGenericParameter);
            Assert.IsEmpty(irMethod.Parameters);
        }

        [Test]
        public void InterpretMethod_MethodWithGenericParametersAndReturnType()
        {
            var methodInfo = typeof(MethodTestClass).GetMethod("MethodWithGenericParametersAndReturnType");
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("MethodWithGenericParametersAndReturnType", irMethod.Name);
            Assert.IsTrue(irMethod.ReturnType.BackingType.IsGenericParameter);
            Assert.AreEqual(1, irMethod.Parameters.Count);
            Assert.AreEqual("param", irMethod.Parameters[0].Name);
            Assert.IsTrue(irMethod.Parameters[0].Type.BackingType.IsGenericParameter);
        }

        [Test]
        public void InterpretMethod_MethodWithGenericParametersAndReturnTypeAndParameters()
        {
            var methodInfo = typeof(MethodTestClass).GetMethod("MethodWithGenericParametersAndReturnTypeAndParameters");
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("MethodWithGenericParametersAndReturnTypeAndParameters", irMethod.Name);
            Assert.IsTrue(irMethod.ReturnType.BackingType.IsGenericParameter);
            Assert.AreEqual(2, irMethod.Parameters.Count);
            Assert.AreEqual("param1", irMethod.Parameters[0].Name);
            Assert.IsTrue(irMethod.Parameters[0].Type.BackingType.IsGenericParameter);
            Assert.AreEqual("param2", irMethod.Parameters[1].Name);
            Assert.IsTrue(irMethod.Parameters[1].Type.BackingType.IsGenericParameter);
        }

        [Test]
        public void InterpretMethod_MethodWithTaskReturnType()
        {
            var methodInfo = typeof(MethodTestClass).GetMethod("MethodWithTaskReturnType");
            var irMethod = MethodInterpreter.InterpretMethod(typeGraph, methodInfo);

            Assert.AreEqual("MethodWithTaskReturnType", irMethod.Name);
            Assert.AreEqual(typeof(Task), irMethod.ReturnType);
            Assert.IsEmpty(irMethod.Parameters);
        }
    }
}