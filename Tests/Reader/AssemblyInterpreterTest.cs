using System.Reflection;
using NUnit.Framework;
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
        public void
            GenerateIntermediateRepresentation_ShouldReturnIRAssemblyWithNoClasses_WhenAssemblyDefinedTypesAreNotPublic()
        {
            // Arrange
            var assembly = Assembly.Load("Assembly.With.No.Public.Types"); //An assembly with no public types

            // Act
            var result = AssemblyInterpreter.GenerateIntermediateRepresentation(assembly);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Classes);
        }

        [Test]
        public void GenerateIntermediateRepresentation_ShouldThrowArgumentNullException_WhenAssemblyIsNull()
        {
            // Arrange
            Assembly assembly = null;

            // Act & Assert
            Assert.Throws<System.ArgumentNullException>(() =>
                AssemblyInterpreter.GenerateIntermediateRepresentation(assembly));
        }
    }
}