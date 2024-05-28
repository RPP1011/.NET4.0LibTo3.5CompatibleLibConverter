﻿using System;
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
                string interfaceName = "I" + GetBaseTypeName(type);
                string namespaceName = type.Namespace;

                var namespaces = new HashSet<string> { "System" };
                namespaces.UnionWith(GetMethodNamespaces(type));

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
                        string returnType = GetFriendlyName(method.ReturnType);
                        string parameters = string.Join(", ",
                            method.GetParameters().Select(p =>
                                $"{(p.ParameterType.IsByRef ? "ref " : "")}{GetFriendlyName(p.ParameterType)} {p.Name}"));

                        if (returnType.StartsWith("Task")) // Handle Task-based methods
                        {
                            returnType = "void";
                            parameters = string.IsNullOrWhiteSpace(parameters)
                                ? "AsyncCallback<" + GetGenericType(method.ReturnType) + "> callback"
                                : parameters + ", AsyncCallback<" + GetGenericType(method.ReturnType) + "> callback";
                            writer.WriteLine($"        {returnType} {method.Name}({parameters});");
                        }
                        else
                        {
                            writer.WriteLine($"        {returnType} {method.Name}({parameters});");
                        }
                    }

                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                }
            }
        }

        private static string GetBaseTypeName(Type type)
        {
            string baseName = type.Name;
            if (type.IsGenericType)
            {
                baseName = type.Name.Substring(0, type.Name.IndexOf('`'));
            }

            return baseName;
        }

        private static string GetFriendlyName(Type type)
        {
            if (type.IsGenericType)
            {
                string genericType = type.GetGenericTypeDefinition().Name;
                genericType = genericType.Substring(0, genericType.IndexOf('`'));
                string genericArgs = string.Join(", ", type.GetGenericArguments().Select(GetFriendlyName));
                return $"{genericType}<{genericArgs}>";
            }

            if (type.IsByRef)
            {
                return GetFriendlyName(type.GetElementType());
            }

            if (type.IsInterface && type.Namespace != null && type.Namespace.StartsWith("Steamworks"))
            {
                return "I" + type.Name;
            }

            return type.Name;
        }

        private static string GetGenericType(Type type)
        {
            if (type.IsGenericType)
            {
                return GetFriendlyName(type.GetGenericArguments()[0]);
            }

            return "object";
        }

        private static IEnumerable<string> GetMethodNamespaces(Type type)
        {
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance |
                                                          BindingFlags.DeclaredOnly))
            {
                yield return method.ReturnType.Namespace;
                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    yield return parameter.ParameterType.Namespace;
                }
            }
        }
    }
}