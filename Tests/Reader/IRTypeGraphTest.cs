using NUnit.Framework;
using WrapperGenerator.Reader;

namespace Tests.Reader
{
    public class IRTypeGraphTests
    {
        private IRTypeGraph _irTypeGraph;

        [SetUp]
        public void Setup()
        {
            _irTypeGraph = new IRTypeGraph();
        }

        [Test]
        public void GetIrType_WhenCalledWithNewType_ReturnsNewIRType()
        {
            // Arrange
            var testType = typeof(string);

            // Act
            var result = _irTypeGraph.GetIrType(testType);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testType, result.BackingType);
        }

        [Test]
        public void GetIrType_WhenCalledWithExistingType_ReturnsExistingIRType()
        {
            // Arrange
            var testType = typeof(string);
            var firstCallResult = _irTypeGraph.GetIrType(testType);

            // Act
            var secondCallResult = _irTypeGraph.GetIrType(testType);

            // Assert
            Assert.AreSame(firstCallResult, secondCallResult);
        }

        [Test]
        public void GetIrType_WhenCalledWithDifferentTypes_ReturnsDifferentIRTpyes()
        {
            // Arrange
            var firstType = typeof(string);
            var secondType = typeof(int);

            // Act
            var firstResult = _irTypeGraph.GetIrType(firstType);
            var secondResult = _irTypeGraph.GetIrType(secondType);

            // Assert
            Assert.IsNotNull(firstResult);
            Assert.IsNotNull(secondResult);
            Assert.AreNotEqual(firstResult, secondResult);
        }
    }
}