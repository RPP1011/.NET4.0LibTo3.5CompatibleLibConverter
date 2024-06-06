using System;
using System.Collections.Generic;
using WrapperGenerator.IR;

namespace WrapperGenerator.Reader
{
    public class IRTypeGraph
    {
        private readonly Dictionary<Type, IRType> _intermediateRepresentations = new Dictionary<Type, IRType>();

        public IRType GetIrType(Type type)
        {
            if (_intermediateRepresentations.TryGetValue(type, out var intermediateRepresentation))
            {
                return intermediateRepresentation;
            }

            var irType = new IRType
            {
                BackingType = type
            };
            _intermediateRepresentations[type] = irType;
            return irType;
        }
    }
}