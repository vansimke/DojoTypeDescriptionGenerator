using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DojoTypeDescriptor.Models;

namespace DojoTypeDescriptor.Generators
{
    public class PackageGenerator
    {
        

        public static string Generate(DojoPackage obj)
        {
            string result = "";

            if (obj.Name != null && obj.Name.Trim().Length > 0)
            {
                string value = GeneratorCommon.GetIndentLevel() == 0 ? "declare " : "";
                value += "module ";

                value += GeneratorCommon.GetShortName(obj.Name) + " {";
                result += GeneratorCommon.AddLine(value);
                GeneratorCommon.IncreaseIndent();
            }
            obj.Functions.ForEach(f => result += FunctionGenerator.Generate(f));
            obj.Classes.ForEach(c => result += ClassGenerator.Generate(c));
            obj.Packages.ForEach(package => result += Generate(package));
            obj.Objects.ForEach(o => result += ObjectGenerator.Generate(o));

            if (obj.Name != null && obj.Name.Trim().Length > 0)
            {
                GeneratorCommon.DecreaseIndent();
                result += GeneratorCommon.AddLine("}") + "\n";
            }

            return result;
        }
        
        public static string Generate(DojoVariable obj)
        {
            string result = "";

            return result;
        }

        public static string WriteModuleDeclares(DojoPackage obj)
        {
            string result = "";

            obj.Functions.ForEach(f => result += FunctionGenerator.WriteModuleDeclares(f));
            obj.Objects.ForEach(o => result += ObjectGenerator.WriteModuleDeclares(o));
            obj.Classes.ForEach(c => result += ClassGenerator.WriteModuleDeclares(c));
            obj.Packages.ForEach(p => result += PackageGenerator.WriteModuleDeclares(p));

            return result;
        }

        
    }
}