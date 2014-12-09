using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DojoTypeDescriptor.Models;

namespace DojoTypeDescriptor.Generators
{
    public class ObjectGenerator
    {
        public static string Generate(DojoObject obj)
        {

            string result = "";
            result += GeneratorCommon.AddComment(obj.Description, null, obj.Permalink);

            string value = "";
            value += "interface ";
            value += GeneratorCommon.GetShortName(obj.Name);



            value += " {";

            result += GeneratorCommon.AddLine(value);
            GeneratorCommon.IncreaseIndent();

            result += GenerateProperties(obj.Properties);
            obj.Methods.ForEach(m => result += GenerateMethod(m));

            GeneratorCommon.DecreaseIndent();

            result += GeneratorCommon.AddLine("}");


            if (obj.ChildClasses.Count > 0 || obj.ChildObjects.Count > 0 || obj.ChildFunctions.Count > 0 || obj.ChildInstances.Count > 0 || obj.ChildPackages.Count > 0)
            {
                result += GeneratorCommon.AddLine("module " + GeneratorCommon.GetShortName(obj.Name) + " {");

                GeneratorCommon.IncreaseIndent();
                obj.ChildClasses.ForEach(cc => result += ClassGenerator.Generate(cc));
                obj.ChildObjects.ForEach(co => result += ObjectGenerator.Generate(co));
                obj.ChildFunctions.ForEach(f => result += FunctionGenerator.Generate(f));
                obj.ChildInstances.ForEach(ci => result += InstanceGenerator.Generate(ci));
                obj.ChildPackages.ForEach(p => result += PackageGenerator.Generate(p));

                GeneratorCommon.DecreaseIndent();
                result += GeneratorCommon.AddLine("}") + "\n";
            }

            return result;
        }

        private static string GenerateMethod(DojoFunction m)
        {
            string result = "";

            if (new List<string>() { "|" }.Contains(m.Name))
            {
                return result;
            }

            string comment = GeneratorCommon.AddComment(m.Description, m.Parameters);
            string functionSignature = "";
            functionSignature += GeneratorCommon.AddLine(GeneratorCommon.GetShortName(m.Name) + "({0}): {1};");

            var parameters = new Dictionary<string, List<string>>();
            var argumentLists = new List<string>();
            argumentLists.Add("");
            if (m.Parameters != null && m.Parameters.Count() > 0)
            {
                m.Parameters.ForEach(p =>
                {
                    p.Types.ForEach(t =>
                    {
                        var name = GeneratorCommon.EscapeKeywords(p.Name);
                        if (p.IsOptional)
                        {
                            name += "?";
                        }
                        if (!parameters.ContainsKey(name))
                        {
                            parameters[name] = new List<string>();
                        }
                        parameters[name].Add(GeneratorCommon.SlashesToPeriods(t));
                    });
                });
                foreach (var parameterName in parameters.Keys)
                {
                    var newArgumentLists = new List<string>();

                    parameters[parameterName].ForEach(t =>
                    {
                        argumentLists.ForEach(argList =>
                        {
                            string newArgList = argList;
                            newArgList += argList.Length > 0 ? ", " : "";
                            newArgList += parameterName + ": " + t;
                            newArgumentLists.Add(newArgList);
                        });

                    });
                    argumentLists = newArgumentLists;

                }
            }

            var returnTypes = new List<string>();
            if (m.ReturnTypes != null && m.ReturnTypes.Count() > 0)
            {

                m.ReturnTypes.ForEach(rt => rt.Types.ForEach(t =>
                {
                    if (t == null || t == "null")
                    {
                        t = "void";
                    }
                    returnTypes.Add(GeneratorCommon.SlashesToPeriods(t.Trim()));
                }));
            }
            else
            {
                returnTypes.Add("void");
            }

            argumentLists.Distinct().ToList().ForEach(al =>
            {
                result += comment;
                result += functionSignature.Replace("{0}", al).Replace("{1}", returnTypes.First());
            });

            return result;
        }

        private static string GenerateProperties(List<DojoVariable> properties)
        {
            string result = "";

            if (properties != null)
            {
                properties.ForEach(p =>
                {
                    result += GeneratorCommon.AddComment(p.Description);
                    string type = p.Types.First();
                    result += GeneratorCommon.AddLine(p.Name + ": " + type + ";");
                });
            }

            return result;
        }

        public static string WriteModuleDeclares(DojoObject obj)
        {
            var result = "";

            result += GeneratorCommon.AddLine("declare module \"" + obj.Name + "\" {");
            GeneratorCommon.IncreaseIndent();
            result += GeneratorCommon.AddLine(string.Format("var exp: {0};", GeneratorCommon.EscapeKeywordsInModuleDeclarations(obj.Name).Replace("/", ".").Replace("-", "_")));
            result += GeneratorCommon.AddLine("export=exp;");
            GeneratorCommon.DecreaseIndent();
            result += GeneratorCommon.AddLine("}");

            obj.ChildClasses.ForEach(c => result += ClassGenerator.WriteModuleDeclares(c));
            obj.ChildFunctions.ForEach(f => result += FunctionGenerator.WriteModuleDeclares(f));
            obj.ChildObjects.ForEach(o => result += ObjectGenerator.WriteModuleDeclares(o));
            obj.ChildPackages.ForEach(p => result += PackageGenerator.WriteModuleDeclares(p));

            return result;
        }
    }
}