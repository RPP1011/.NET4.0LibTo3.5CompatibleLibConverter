using System.Reflection;
using WrapperGenerator.IR;

namespace WrapperGenerator.Reader
{
    public class FieldInterpreter
    {
        public static IRField InterpretField(FieldInfo fieldInfo)
        {
            return new IRField()
            {
                Name = fieldInfo.Name,
                FieldAttributes = fieldInfo.Attributes
            };
        }
    }
}