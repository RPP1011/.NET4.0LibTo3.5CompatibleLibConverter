using System.Reflection;
using WrapperGenerator.IR;

namespace WrapperGenerator.Reader
{
    public class FieldInterpreter
    {
        public static IRField InterpretField(IRTypeGraph typeGraph, FieldInfo fieldInfo)
        {
            return new IRField
            {
                Name = fieldInfo.Name,
                Type = typeGraph.GetIrType(fieldInfo.GetType()),
                FieldAttributes = fieldInfo.Attributes
            };
        }
    }
}