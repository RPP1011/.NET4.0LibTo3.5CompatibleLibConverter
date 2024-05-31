using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WrapperGenerator.IR;

namespace WrapperGenerator.Reader
{
    public class ClassInterpreter
    {
        public static IRClass InterpretType(TypeInfo type)
        {
            return new IRClass()
            {
                GenericTypes = new List<Type>(type.GetGenericArguments()),
                Methods = type.DeclaredMethods.Where(info => info.IsPublic).Select(MethodIntepreter.InterpretMethod).ToList(),
                Name = type.Name,
                Namespace = "Wrapper" + type.Namespace
            };
        }
    }
}