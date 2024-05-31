using System;
using System.Reflection;

namespace WrapperGenerator.IR
{
    public class IRParameter
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public bool IsByRef { get; set; }
    }
}