using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WrapperGenerator
{
    public class InterfaceGenerator
    {
        public static void GenerateInterfaces(string assemblyPath, string outputPath)
        {
            Assembly sdkAssembly = Assembly.LoadFrom(assemblyPath);

            foreach (Type type in sdkAssembly.GetTypes().Where(t => t.IsPublic))
            {
                string interfaceName = "I" + CodeGenerationUtils.GetBaseTypeName(type);
                string namespaceName = type.Namespace;

                var namespaces = new HashSet<string> { "System" };
                namespaces.UnionWith(CodeGenerationUtils.GetMethodNamespaces(type));

                string directoryPath =
                    Path.Combine(outputPath, namespaceName.Replace('.', Path.DirectorySeparatorChar));
                Directory.CreateDirectory(directoryPath);

                using (StreamWriter writer = new StreamWriter(Path.Combine(directoryPath, interfaceName + ".cs")))
                {
                    foreach (var ns in namespaces)
                    {
                        writer.WriteLine($"using {ns};");
                    }

                    writer.WriteLine();
                    writer.WriteLine($"namespace {namespaceName}");
                    writer.WriteLine("{");
                    writer.WriteLine($"    public interface {interfaceName}");
                    writer.WriteLine("    {");

                    foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance |
                                                                  BindingFlags.DeclaredOnly))
                    {
                        writer.WriteLine($"        {CodeGenerationUtils.GenerateMethodSignature(method)}");
                    }

                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                }

            }
        }
    }
}