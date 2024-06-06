using System.Reflection;
using WrapperGenerator.IR;

namespace WrapperGenerator.Reader
{
    public class ParameterInterpreter
    {
        public static IRParameter InterpretParameter(IRTypeGraph typeGraph, ParameterInfo parameterInfo)
        {
            return new IRParameter
            {
                IsByRef = parameterInfo.ParameterType.IsByRef,
                Name = parameterInfo.Name,
                Type = typeGraph.GetIrType(parameterInfo.ParameterType)
            };
        }
    }
}