using System.Threading.Tasks;

namespace Tests.TestAssembly
{
    // Test class for purpose of testing the AssemblyInterpreter
    public class TestClass
    {
        private string privatePrimitive;
        private TestClass privateReference;
        public string publicPrimitive;
        public TestClass publicReference;

        public Task<TestClass> testClassTask;

        public void PublicMethod()
        {
            // Method logic here
        }

        private void PrivateMethod()
        {
        }
    }
}