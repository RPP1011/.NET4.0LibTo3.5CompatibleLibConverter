using System;
using System.Collections.Generic;
using System.Reflection;

namespace WrapperGenerator.IR
{
    public class IRClass
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public List<IRMethod> Methods { get; set; } = new List<IRMethod>();
        public List<Type> GenericTypes { get; set; }
    }
}