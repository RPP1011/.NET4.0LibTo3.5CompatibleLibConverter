using System.Linq;
using System.Reflection;
using WrapperGenerator.IR;

namespace WrapperGenerator.Reader
{
    public class MethodInterpreter
    {
        public static IRMethod InterpretMethod(IRTypeGraph typeGraph, MethodInfo methodInfo)
        {
            IRParameter InterpretParameter(ParameterInfo parameterInfo) => ParameterInterpreter.InterpretParameter(typeGraph, parameterInfo);
            return new IRMethod()
            {
                Name = methodInfo.Name,
                Parameters = methodInfo.GetParameters().Select(InterpretParameter).ToList(),
                ReturnType = typeGraph.GetIrType(methodInfo.ReturnType)
            };
        }
    }
}