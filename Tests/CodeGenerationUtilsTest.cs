using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WrapperGenerator;

namespace Tests
{
    [TestFixture]
    [TestOf(typeof(CodeGenerationUtils))]
    public class CodeGenerationUtilsTest
    {
        public class DummyClass
        {
            public string ExampleMethod(string arg1, ref double arg2)
            {
                return string.Empty;
            }
            
            public void ExampleVoidMethod(string arg1, ref double arg2)
            {
            }

        }

        [Test]
        public void TestGetBaseTypeName()
        {
            string cleanTypeName = CodeGenerationUtils.GetBaseTypeName(typeof(Dictionary<string, string[]>));
            Assert.AreEqual("Dictionary", cleanTypeName);

            cleanTypeName = CodeGenerationUtils.GetBaseTypeName(typeof(int));
            Assert.AreEqual("Int32", cleanTypeName);
        }

        [Test]
        public void TestGetFriendlyName()
        {
            string friendlyName = CodeGenerationUtils.GetFriendlyName(typeof(Dictionary<string, string[]>));
            Assert.AreEqual("Dictionary<String, String[]>", friendlyName);

            friendlyName = CodeGenerationUtils.GetFriendlyName(typeof(DummyClass));
            Assert.AreEqual("DummyClass", friendlyName);
        }

        [Test]
        public void TestGetGenericType()
        {
            string genericType = CodeGenerationUtils.GetGenericType(typeof(Dictionary<string, string[]>));
            Assert.AreEqual("Pair<String, String[]>", genericType);

            genericType = CodeGenerationUtils.GetGenericType(typeof(int));
            Assert.AreEqual("object", genericType);
        }

        [Test]
        public void TestGetMethodNamespaces()
        {
            var namespaces = CodeGenerationUtils.GetMethodNamespaces(typeof(DummyClass));

            Assert.True(namespaces.Contains("System"));
        }

        [Test]
        public void TestGenerateMethodSignature()
        {
            MethodInfo mi = typeof(DummyClass).GetMethod("ExampleMethod");
            string signature = CodeGenerationUtils.GenerateMethodSignature(mi);

            Assert.AreEqual("String ExampleMethod(String arg1, ref Double arg2);", signature);
        }

        [Test]
        public void TestGenerateWrapperMethodImplementation()
        {
            MethodInfo mi = typeof(DummyClass).GetMethod("ExampleMethod");
            string implementation = CodeGenerationUtils.GenerateWrapperMethodImplementation(mi, "dummyInstance");

            Assert.AreEqual(@"public String ExampleMethod(String arg1, ref Double arg2){return dummyInstance.ExampleMethod(arg1, arg2);}", implementation);
        }

        [Test]
        public void TestGenerateMethodSignatureForVoidMethod()
        {
            MethodInfo mi = typeof(DummyClass).GetMethod("ExampleVoidMethod");
            string signature = CodeGenerationUtils.GenerateMethodSignature(mi);

            Assert.AreEqual("void ExampleVoidMethod(String arg1, ref Double arg2);", signature);
        }
    }
}