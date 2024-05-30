using System.Collections.Generic;

namespace WrapperGenerator.Types
{
    public class IntermediateParameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsByRef { get; set; }

        public override string ToString() => $"{(IsByRef ? "ref " : "")}{Type} {Name}";
    }

    public class IntermediateMethod
    {
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public List<IntermediateParameter> Parameters { get; set; } = new List<IntermediateParameter>();

        public override string ToString()
        {
            var parameters = string.Join(", ", Parameters);
            return $"{ReturnType} {Name}({parameters});";
        }
    }

    public class IntermediateClass
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public List<IntermediateMethod> Methods { get; set; } = new List<IntermediateMethod>();
    }

}