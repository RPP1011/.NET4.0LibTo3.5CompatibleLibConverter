using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WrapperGenerator.Types;
using MethodInfo = WrapperGenerator.Types.MethodInfo;
using ParameterInfo = WrapperGenerator.Types.ParameterInfo;

namespace WrapperGenerator
{
    public static class CodeGenerationUtils
    {
        private static readonly Dictionary<Type, string> TypeMappings = new Dictionary<Type, string>
        {
            { typeof(void), "void" },
            // Add more type mappings as needed
        };
        
        public static string GetBaseTypeName(Type type)
        {
            string baseName = type.Name;
            if (type.IsGenericType)
            {
                baseName = type.Name.Substring(0, type.Name.IndexOf('`'));
            }

            return baseName;
        }

        public static string GetFriendlyName(Type type)
        {
            if (TypeMappings.TryGetValue(type, out var friendlyName))
            {
                return friendlyName;
            }

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

        public static string GetGenericType(Type type)
        {
            if (type.IsGenericType)
            {
                return GetFriendlyName(type.GetGenericArguments()[0]);
            }

            return "object";
        }

        public static IEnumerable<string> GetMethodNamespaces(Type type)
        {
            foreach (System.Reflection.MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance |
                                                                            BindingFlags.DeclaredOnly))
            {
                yield return method.ReturnType.Namespace;
                foreach (System.Reflection.ParameterInfo parameter in method.GetParameters())
                {
                    yield return parameter.ParameterType.Namespace;
                }
            }
        }
        
        public static MethodInfo GenerateMethodSignature(System.Reflection.MethodInfo method)
        {
            var methodInfo = new MethodInfo
            {
                Name = method.Name,
                ReturnType = GetFriendlyName(method.ReturnType)
            };

            foreach (var param in method.GetParameters())
            {
                methodInfo.Parameters.Add(new ParameterInfo
                {
                    Name = param.Name,
                    Type = GetFriendlyName(param.ParameterType),
                    IsByRef = param.ParameterType.IsByRef
                });
            }

            if (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                methodInfo.ReturnType = "void";
                var callbackType = GetGenericType(method.ReturnType);
                methodInfo.Parameters.Add(new ParameterInfo
                {
                    Name = "callback",
                    Type = $"AsyncCallback<{callbackType}>",
                    IsByRef = false
                });
            }

            return methodInfo;
        }

        
        public static string GenerateWrapperMethodImplementation(MethodInfo method, string instanceName)
        {
            string returnType = GetFriendlyName(method.ReturnType);
            string parameters = string.Join(", ",
                method.GetParameters().Select(p =>
                    $"{(p.ParameterType.IsByRef ? "ref " : "")}{GetFriendlyName(p.ParameterType)} {p.Name}"));

            if (returnType.StartsWith("Task")) // Handle Task-based methods
            {
                returnType = "void";
                string callbackType = GetGenericType(method.ReturnType);
                string methodCall = string.IsNullOrWhiteSpace(parameters) ? "callback" : parameters + ", callback";
                return $@"
        public {returnType} {method.Name}({parameters}, AsyncCallback<{callbackType}> callback)
        {{
            Task.Run(() =>
            {{
                try
                {{
                    var result = {instanceName}.{method.Name}().Result;
                    callback(result, null);
                }}
                catch (Exception ex)
                {{
                    callback(default, ex);
                }}
            }});
        }}";
            }
            else
            {
                return $@"
        public {returnType} {method.Name}({parameters})
        {{
            return {instanceName}.{method.Name}({string.Join(", ", method.GetParameters().Select(p => p.Name))});
        }}";
            }
        }
    }
}