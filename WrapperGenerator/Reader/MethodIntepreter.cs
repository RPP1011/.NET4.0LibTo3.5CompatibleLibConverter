using System.Linq;
using System.Reflection;
using WrapperGenerator.IR;

namespace WrapperGenerator.Reader
{
    public class MethodIntepreter
    {
        public static IRMethod InterpretMethod(MethodInfo methodInfo)
        {
            return new IRMethod()
            {
                Name = methodInfo.Name,
                Parameters = methodInfo.GetParameters().Select(ParameterInterpreter.InterpretParameter).ToList(),
                ReturnType = methodInfo.ReturnType
            };
        }
    }
}