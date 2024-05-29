using NUnit.Framework;
using WrapperGenerator;
using System;
using System.IO;

namespace Tests
{
    [TestFixture]
    [TestOf(typeof(WrapperGenerator.WrapperGenerator))]
    public class WrapperGeneratorTest
    {
        private string temporaryOutputPath;

        [SetUp]
        public void SetUp()
        {
            temporaryOutputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(temporaryOutputPath);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(temporaryOutputPath, true);
        }

        [Test]
        public void TestGenerateWrappersWithInvalidAssemblyPath_ShouldThrowArgumentException()
        {
            string invalidAssemblyPath = "invalidAssembly.dll";

            var exception = Assert.Throws<ArgumentException>(() =>
                WrapperGenerator.WrapperGenerator.GenerateWrappers(invalidAssemblyPath, temporaryOutputPath));
            StringAssert.StartsWith("The file", exception.Message);
            StringAssert.EndsWith("does not exist.", exception.Message);
        }

        [Test]
        public void TestGenerateWrappersWithEmptyAssemblyPath_ShouldThrowArgumentException()
        {
            string emptyAssemblyPath = "";

            var exception = Assert.Throws<ArgumentException>(() =>
                WrapperGenerator.WrapperGenerator.GenerateWrappers(emptyAssemblyPath, temporaryOutputPath));
            StringAssert.AreEqualIgnoringCase(
                "An empty or whitespace only string was passed to a method which does not allow this.",
                exception.Message);
        }

        [Test]
        public void TestGenerateWrappersWithEmptyOutputPath_ShouldThrowArgumentException()
        {
            string emptyOutputPath = "";

            var exception = Assert.Throws<ArgumentException>(() =>
                WrapperGenerator.WrapperGenerator.GenerateWrappers(emptyOutputPath, temporaryOutputPath));
            StringAssert.AreEqualIgnoringCase(
                "An empty or whitespace only string was passed to a method which does not allow this.",
                exception.Message);
        }

        [Test]
        public void TestGenerateWrappersWithNullAssemblyPath_ShouldThrowArgumentNullException()
        {
            string nullAssemblyPath = null;

            var exception = Assert.Throws<ArgumentNullException>(() =>
                WrapperGenerator.WrapperGenerator.GenerateWrappers(nullAssemblyPath, temporaryOutputPath));
            Assert.AreEqual("assemblyPath", exception.ParamName);
        }

        [Test]
        public void TestGenerateWrappersWithNullOutputPath_ShouldThrowArgumentNullException()
        {
            string nullOutputPath = null;

            var exception = Assert.Throws<ArgumentNullException>(() =>
                WrapperGenerator.WrapperGenerator.GenerateWrappers(temporaryOutputPath, nullOutputPath));
            Assert.AreEqual("outputPath", exception.ParamName);
        }

        // Note: For the successful path test you need to provide valid assembly which can be found in your system.
        //       This is just an example code and will not run as it is.

        [Test]
        public void TestGenerateWrappers_ShouldGenerateFilesInOutputPath()
        {
            string validAssemblyPath = @"C:\Path\To\YourAssembly.dll"; // Replace with your valid assembly path.
            WrapperGenerator.WrapperGenerator.GenerateWrappers(validAssemblyPath, temporaryOutputPath);

            var filesInOutputDirectory = Directory.GetFiles(temporaryOutputPath);
            Assert.IsTrue(filesInOutputDirectory.Length > 0);
        }
    }
}