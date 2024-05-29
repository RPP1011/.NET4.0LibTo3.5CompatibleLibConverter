using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WrapperGenerator
{
    public class WrapperGenerator
    {
        public static void GenerateWrappers(string assemblyPath, string outputPath)
        {
            Assembly sdkAssembly = Assembly.LoadFrom(assemblyPath);

            foreach (Type type in sdkAssembly.GetTypes().Where(t => t.IsPublic))
            {
                string className = CodeGenerationUtils.GetBaseTypeName(type) + "Wrapper";
                string interfaceName = "I" + CodeGenerationUtils.GetBaseTypeName(type);
                string namespaceName = type.Namespace;

                var namespaces = new HashSet<string> { "System", "System.Threading.Tasks" };
                namespaces.UnionWith(CodeGenerationUtils.GetMethodNamespaces(type));

                string directoryPath =
                    Path.Combine(outputPath, namespaceName.Replace('.', Path.DirectorySeparatorChar));
                Directory.CreateDirectory(directoryPath);

                using (StreamWriter writer = new StreamWriter(Path.Combine(directoryPath, className + ".cs")))
                {
                    foreach (var ns in namespaces)
                    {
                        writer.WriteLine($"using {ns};");
                    }

                    writer.WriteLine();
                    writer.WriteLine($"namespace {namespaceName}");
                    writer.WriteLine("{");
                    writer.WriteLine($"    public class {className} : {interfaceName}");
                    writer.WriteLine("    {");
                    writer.WriteLine(
                        $"        private readonly {CodeGenerationUtils.GetFriendlyName(type)} _instance;");
                    writer.WriteLine($"        public {className}()");
                    writer.WriteLine("        {");
                    writer.WriteLine($"            _instance = new {CodeGenerationUtils.GetFriendlyName(type)}();");
                    writer.WriteLine("        }");

                    foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance |
                                                                  BindingFlags.DeclaredOnly))
                    {
                        writer.WriteLine(CodeGenerationUtils.GenerateWrapperMethodImplementation(method, "_instance"));
                    }

                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                }
            }
        }
    }
}