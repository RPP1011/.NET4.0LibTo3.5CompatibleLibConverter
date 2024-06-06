namespace WrapperGenerator.IR
{
    public class IRParameter
    {
        public string Name { get; set; }
        public IRType Type { get; set; }
        public bool IsByRef { get; set; }
    }
}