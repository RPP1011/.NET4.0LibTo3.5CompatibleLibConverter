using System.Collections.Generic;

namespace WrapperGenerator.IR
{
    public class IRClass : IRType
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public List<IRField> Fields { get; set; } = new List<IRField>();
        public List<IRMethod> Methods { get; set; } = new List<IRMethod>();
        public List<IRType> GenericTypes { get; set; }
    }
}