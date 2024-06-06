using System.Linq;
using System.Reflection;
using WrapperGenerator.IR;

namespace WrapperGenerator.Reader
{
    public class ClassInterpreter
    {
        public static IRClass InterpretType(IRTypeGraph typeGraph, TypeInfo type)
        {
            IRMethod InterpretMethod(MethodInfo methodInfo)
            {
                return MethodInterpreter.InterpretMethod(typeGraph, methodInfo);
            }

            IRField InterpretField(FieldInfo fieldInfo)
            {
                return FieldInterpreter.InterpretField(typeGraph, fieldInfo);
            }

            return new IRClass
            {
                GenericTypes = type.GetGenericArguments().Select(typeGraph.GetIrType).ToList(),
                Methods = type.DeclaredMethods.Where(info => info.IsPublic).Select(InterpretMethod).ToList(),
                Fields = type.DeclaredFields.Where(info => info.IsPublic).Select(InterpretField).ToList(),
                Name = type.Name,
                Namespace = "Wrapper" + type.Namespace
            };
        }
    }
}