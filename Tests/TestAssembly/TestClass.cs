using System.Threading.Tasks;

namespace Tests.TestAssembly
{
    // Test class for purpose of testing the AssemblyInterpreter
    public class TestClass
    {
        public string publicPrimitive;
        public TestClass publicReference;
        private string privatePrimitive;
        private TestClass privateReference;
        
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