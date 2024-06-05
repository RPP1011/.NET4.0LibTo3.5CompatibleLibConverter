using System;
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
            var typeGraph = new IRTypeGraph();
            IRClass InterpretType(TypeInfo type) => ClassInterpreter.InterpretType(typeGraph, type);

            return new IRAssembly()
            {
                Classes = assembly.DefinedTypes.Where(type => type.IsPublic)
                    .Select((Func<TypeInfo, IRClass>)InterpretType)
                    .ToList()
            };
        }
    }
}