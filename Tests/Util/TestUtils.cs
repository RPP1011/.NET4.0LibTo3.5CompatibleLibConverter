using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Tests.TestAssembly;

namespace Tests.Util
{
    public class TestUtils
    {
        public static Assembly GenerateTestAssemblyForClasses(params Type[] types)
        {
            // Create a new assembly
            var assemblyName = new AssemblyName("TestAssembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            
            // Add all Tests.TestAssembly classes/enums to assembly
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("TestModule");
            
            foreach (var type in types.Where(type => type.Namespace == "Tests.TestAssembly"))
            {
                var typeBuilder = moduleBuilder.DefineType(type.Name, type.Attributes);
                foreach (var field in type.GetFields())
                {
                    typeBuilder.DefineField(field.Name, field.FieldType, field.Attributes);
                }

                typeBuilder.CreateType();
            }
            
            // Build assembly
            return moduleBuilder.Assembly;
        }

        public static Assembly GenerateTestAssembly()
        {
            // Create a new assembly
            var assemblyName = new AssemblyName("TestAssembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            
            // Add all Tests.TestAssembly classes/enums to assembly
            var testAssembly = Assembly.GetAssembly(typeof(TestClass));
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("TestModule");
            
            foreach (var type in testAssembly.GetTypes().Where(type => type.Namespace == "Tests.TestAssembly"))
            {
                var typeBuilder = moduleBuilder.DefineType(type.Name, type.Attributes);
                foreach (var field in type.GetFields())
                {
                    typeBuilder.DefineField(field.Name, field.FieldType, field.Attributes);
                }

                typeBuilder.CreateType();
            }
            
            // Build assembly
            return moduleBuilder.Assembly;
        }
    }
}