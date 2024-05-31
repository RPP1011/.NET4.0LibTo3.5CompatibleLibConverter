using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WrapperGenerator.IR;

namespace WrapperGenerator.Reader
{
    public class AssemblyInterpreter
    {
        public static IRAssembly GenerateIntermediateRepresentation(Assembly assembly)
        {
            return new IRAssembly()
            {
                Classes = assembly.DefinedTypes.Where(type => type.IsPublic)
                    .Select(ClassInterpreter.InterpretType)
                    .ToList()
            };
        }
    }
}