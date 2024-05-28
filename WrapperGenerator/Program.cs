using System;

namespace WrapperGenerator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string sdkAssemblyPath = @"C:\Project\Wrapper\net46\Facepunch.Steamworks.Win64.dll";
            string interfaceOutputPath = @"C:\Users\richa\RiderProjects\WrapperGenerator\WrapperGenerator\Interfaces";
            string wrapperOutputPath = @"C:\Users\richa\RiderProjects\WrapperGenerator\WrapperGenerator\Wrappers";

            SdkAnalyzer.AnalyzeSdk(sdkAssemblyPath);
            InterfaceGenerator.GenerateInterfaces(sdkAssemblyPath, interfaceOutputPath);
            WrapperGenerator.GenerateWrappers(sdkAssemblyPath, wrapperOutputPath);

            Console.WriteLine("Wrapper generation completed.");
        }
    }
}