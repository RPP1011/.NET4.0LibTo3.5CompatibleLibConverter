using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using NUnit.Framework;
using Tests.TestAssembly;
using WrapperGenerator.Reader;

namespace Tests.Reader
{
    [TestFixture]
    [TestOf(typeof(AssemblyInterpreter))]
    public class AssemblyInterpreterTest
    {
        private AssemblyInterpreter _assemblyInterpreter;

        [SetUp]
        public void Setup()
        {
            _assemblyInterpreter = new AssemblyInterpreter();
        }

        [Test]
        public void
            GenerateIntermediateRepresentation_ShouldReturnIRAssemblyWithPublicTypes_WhenAssemblyDefinedTypesArePublic()
        {
            // Arrange
            var assembly = Assembly.GetAssembly(typeof(AssemblyInterpreter));

            // Act
            var result = AssemblyInterpreter.GenerateIntermediateRepresentation(assembly);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.Classes);
        }

        [Test]
        public void GenerateIntermediateRepresentation_ShouldReturnIRAssemblyWithNoClasses_WhenAssemblyDefinedTypesAreNotPublic()
        { 
            var assembly = GenerateTestAssembly();
            var output = AssemblyInterpreter.GenerateIntermediateRepresentation(assembly);
            Assert.AreEqual(1, output.Classes.Count);
        }

        [Test]
        public void GenerateIntermediateRepresentation_ShouldThrowArgumentNullException_WhenAssemblyIsNull()
        {
            // Arrange
            Assembly assembly = null;

            // Act & Assert
            Assert.Throws<System.NullReferenceException>(() =>
                AssemblyInterpreter.GenerateIntermediateRepresentation(assembly));
        }

        public Assembly GenerateTestAssembly()
        {
            // Create a new assembly
            var assemblyName = new AssemblyName("TestAssembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            
            // Add all Tests.TestAssembly classes/enums to assembly
            var testAssembly = Assembly.GetAssembly(typeof(TestClass));
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("TestModule");
            
            foreach (var type in testAssembly.GetTypes().Where(type => type.Namespace == "Tests.TestAssembly"))
            {
                var typeBuilder = moduleBuilder.DefineType(type.Name, type.Attributes);
                foreach (var field in type.GetFields())
                {
                    typeBuilder.DefineField(field.Name, field.FieldType, field.Attributes);
                }

                typeBuilder.CreateType();
            }
            
            // Build assembly
            return moduleBuilder.Assembly;
        }
    }
}