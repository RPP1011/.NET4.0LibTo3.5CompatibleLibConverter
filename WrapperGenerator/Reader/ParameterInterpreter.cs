using System.Reflection;
using WrapperGenerator.IR;

namespace WrapperGenerator.Reader
{
    public class ParameterInterpreter
    {
        public static IRParameter InterpretParameter(ParameterInfo parameterInfo)
        {
            return new IRParameter()
            {
                IsByRef = parameterInfo.ParameterType.IsByRef,
                Name = parameterInfo.Name,
                Type = parameterInfo.ParameterType
            };
        }
    }
}