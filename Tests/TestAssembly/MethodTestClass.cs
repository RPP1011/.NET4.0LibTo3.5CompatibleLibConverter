using System.Threading.Tasks;

namespace Tests.TestAssembly
{
    public class MethodTestClass
    {
        // Public method
        public void PublicMethod()
        {
            // Method logic here
        }

        // Private method
        private void PrivateMethod()
        {
            // Method logic here
        }

        // Void method
        public void VoidMethod()
        {
            // Method logic here
        }

        // Method with parameters
        public void MethodWithParameters(int param1, string param2)
        {
            // Method logic here
        }

        // Method with return type
        public int MethodWithReturnType()
        {
            // Method logic here
            return 42;
        }

        // Method with parameters and return type
        public string MethodWithParametersAndReturnType(int param1, string param2)
        {
            // Method logic here
            return param2 + param1.ToString();
        }

        // Method with generic parameters
        public void MethodWithGenericParameters<T>(T param)
        {
            // Method logic here
        }

        // Method with generic return type
        public T MethodWithGenericReturnType<T>()
        {
            // Method logic here
            return default(T);
        }

        // Method with generic parameters and return type
        public T MethodWithGenericParametersAndReturnType<T>(T param)
        {
            // Method logic here
            return param;
        }

        // Method with generic parameters, return type, and parameters
        public TResult MethodWithGenericParametersAndReturnTypeAndParameters<T1, T2, TResult>(T1 param1, T2 param2)
        {
            // Method logic here
            return default(TResult);
        }

        // Method with Task return type
        public async Task MethodWithTaskReturnType()
        {
            // Method logic here
            await Task.CompletedTask;
        }
    }
}