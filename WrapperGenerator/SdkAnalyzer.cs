using System;
using System.Linq;
using System.Reflection;

namespace WrapperGenerator
{
    public class SdkAnalyzer
    {
        public static void AnalyzeSdk(string assemblyPath)
        {
            Assembly sdkAssembly = Assembly.LoadFrom(assemblyPath);

            foreach (Type type in sdkAssembly.GetTypes().Where(t => t.IsPublic))
            {
                Console.WriteLine($"Class: {type.Name}");
                foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    Console.WriteLine($"  Method: {method.Name}");
                }
            }
        }
    }
}