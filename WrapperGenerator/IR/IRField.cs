using System;
using System.Reflection;

namespace WrapperGenerator.IR
{
    public class IRField
    {
        public IRType Type { get; set; }
        public string Name { get; set; }
        public FieldAttributes FieldAttributes { get; set; }
    }
}