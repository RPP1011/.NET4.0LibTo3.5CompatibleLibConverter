using System;
using System.Reflection;
using NUnit.Framework;
using Tests.Util;
using WrapperGenerator.Reader;

namespace Tests.Reader
{
    [TestFixture]
    [TestOf(typeof(AssemblyInterpreter))]
    public class AssemblyInterpreterTest
    {
        [SetUp]
        public void Setup()
        {
            _assemblyInterpreter = new AssemblyInterpreter();
        }

        private AssemblyInterpreter _assemblyInterpreter;

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
        public void
            GenerateIntermediateRepresentation_ShouldReturnIRAssemblyWithNoClasses_WhenAssemblyDefinedTypesAreNotPublic()
        {
            var assembly = TestUtils.GenerateTestAssembly();
            var output = AssemblyInterpreter.GenerateIntermediateRepresentation(assembly);
            Assert.AreEqual(1, output.Classes.Count);
        }

        [Test]
        public void GenerateIntermediateRepresentation_ShouldThrowArgumentNullException_WhenAssemblyIsNull()
        {
            // Arrange
            Assembly assembly = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() =>
                AssemblyInterpreter.GenerateIntermediateRepresentation(assembly));
        }
    }
}