using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_BuildType
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public static void BUildType()
        {
            AssemblyName aname = new AssemblyName("MyDynamicAssembly");
            AssemblyBuilder assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname,
                                            AssemblyBuilderAccess.Run);
            ModuleBuilder modBuilder = assemBuilder.DefineDynamicModule("DynModule");

            TypeBuilder tb = modBuilder.DefineType("Widget", TypeAttributes.Public);

            MethodBuilder methBuilder = tb.DefineMethod("SayHello", MethodAttributes.Public, null, null);
            ILGenerator gen = methBuilder.GetILGenerator();
            gen.EmitWriteLine("Hello world");
            gen.Emit(OpCodes.Ret);

            Type t = tb.CreateType();

            object o = Activator.CreateInstance(t);
            t.GetMethod("SayHello").Invoke(o, null);

            FieldBuilder field = tb.DefineField("length", typeof(int), FieldAttributes.Private);
            PropertyBuilder prop = tb.DefineProperty(
                                    "Text", // Name of property
                                    PropertyAttributes.None,
                                    typeof(string), // Property type
                                    new Type[0]); // Indexer types


        }
    }
}
