using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dynmic_field
{
    class Program
    {
        static void Main(string[] args)
        {

        }


        //string _text;
        //public string Text
        //{
        //    get => _text;
        //    internal set => _text = value;
        //}
        public static void CreateField()
        {
            AssemblyName aname = new AssemblyName("MyDynamicAssembly");
            AssemblyBuilder assemBuilder = AssemblyBuilder.DefineDynamicAssembly(aname,
                                            AssemblyBuilderAccess.Run);
            ModuleBuilder modBuilder = assemBuilder.DefineDynamicModule("DynModule");

            TypeBuilder tb = modBuilder.DefineType("Widget", TypeAttributes.Public);

            FieldBuilder field = tb.DefineField("_text", typeof(string), FieldAttributes.Private);
            PropertyBuilder prop = tb.DefineProperty(
            "Text", // Name of property
            PropertyAttributes.None,
            typeof(string), //Property type
            new Type[0]); //Indexer types

            MethodBuilder getter = tb.DefineMethod(
            "get_Text", //Method name
            MethodAttributes.Public | MethodAttributes.SpecialName,
            typeof(string), //Return type
            new Type[0]); //Parameter types

            ILGenerator getGen = getter.GetILGenerator();
            getGen.Emit(OpCodes.Ldarg_0); // Load "this" onto eval stack
            getGen.Emit(OpCodes.Ldfld, field); // Load field value onto eval stack
            getGen.Emit(OpCodes.Ret); // Return
            MethodBuilder setter = tb.DefineMethod(
            "set_Text",
            MethodAttributes.Assembly | MethodAttributes.SpecialName,
            null, // Return type
            new Type[] { typeof(string) }); // Parameter types
            ILGenerator setGen = setter.GetILGenerator();
            setGen.Emit(OpCodes.Ldarg_0); // Load "this" onto eval stack
            setGen.Emit(OpCodes.Ldarg_1); // Load 2nd arg, i.e., value
            setGen.Emit(OpCodes.Stfld, field); // Store value into field
            setGen.Emit(OpCodes.Ret); // return
            prop.SetGetMethod(getter); // Link the get method and property
            prop.SetSetMethod(setter); // Link the set method and property


            Type t = tb.CreateType();
            object o = Activator.CreateInstance(t);
            t.GetProperty("Text").SetValue(o, "Good emissions!", new object[0]);
            string text = (string)t.GetProperty("Text").GetValue(o, null);
            Console.WriteLine(text); // Good emissions!

        }
    }
}
