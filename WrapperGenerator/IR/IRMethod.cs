using System.Collections.Generic;

namespace WrapperGenerator.IR
{
    public class IRMethod
    {
        public string Name { get; set; }
        public IRType ReturnType { get; set; }
        public List<IRParameter> Parameters { get; set; } = new List<IRParameter>();

        public override string ToString()
        {
            var parameters = string.Join(", ", Parameters);
            return $"{ReturnType} {Name}({parameters});";
        }
    }
}