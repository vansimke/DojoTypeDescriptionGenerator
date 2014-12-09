using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DojoTypeDescriptor.Models;

namespace DojoTypeDescriptor.Generators
{
    public class ClassGenerator
    {
        public static string Generate(DojoClass obj)
        {
            if (obj.Name == "dijit/form/Button")
            {
                var i = 1;
            }

            string result = "";
            result += GeneratorCommon.AddComment(obj.Description, obj.Usage == null ? null : obj.Usage.Parameters, obj.Permalink);

            string value = "";
            value += "class ";
            value += GeneratorCommon.GetShortName(obj.Name);
            if (obj.BaseClass != null && obj.BaseClass.Length > 0)
            {
                value += " extends " + GeneratorCommon.SlashesToPeriods(obj.BaseClass);
            }

            if (obj.Mixins != null && obj.Mixins.Count() > 0)
            {
                value += " implements ";
                for (var i = 0; i < obj.Mixins.Count(); i++)
                {
                    if (i > 0)
                    {
                        value += ", ";
                    }
                    value += GeneratorCommon.SlashesToPeriods(obj.Mixins[i]);
                }
            }

            value += " {";

            result += GeneratorCommon.AddLine(value);
            GeneratorCommon.IncreaseIndent();
            result += GenerateConstructor(obj);

            bool isStateful = obj.Ancestors.Any(a => a.Equals("dojo/Stateful"));

            if (obj.Ancestors.Any(a => a.Equals("dijit/_TemplatedMixin")))
            {
                obj.Methods.Add(new DojoFunction()
                {
                    Name = "getCachedTemplate",
                    Description = "Static method to get a template based on the templatePath or\ntemplateString key",
                    ReturnTypes = new List<DojoVariable>() 
                    { 
                        new DojoVariable() { 
                            Types = new List<string>() {"any"}
                        } 
                    },
                    Properties = new List<DojoVariable>()
                    {
                        new DojoVariable() 
                        {
                            Name = "templateString",
                            Types = new List<string>(){"String"}
                        },
                        new DojoVariable() 
                        {
                            Name = "alwaysUseString",
                            Types = new List<string>(){"boolean"}
                        },
                        new DojoVariable() 
                        {
                            Name = "doc",
                            Types = new List<string>(){"HTMLDocument"}
                        }
                    }
                });
            }
            result += GenerateProperties(obj.Properties, isStateful);
            obj.Methods.ForEach(m => result += GenerateMethod(m));
            obj.Events.ForEach(e => result += GenerateMethod(e));

            GeneratorCommon.DecreaseIndent();

            result += GeneratorCommon.AddLine("}");


            if (obj.ChildClasses.Count() > 0 || obj.ChildObjects.Count > 0)
            {
                result += GeneratorCommon.AddLine("module " + GeneratorCommon.GetShortName(obj.Name) + " {");

                GeneratorCommon.IncreaseIndent();
                obj.ChildClasses.ForEach(cc => result += ClassGenerator.Generate(cc));
                obj.ChildObjects.ForEach(co => result += ObjectGenerator.Generate(co));

                GeneratorCommon.DecreaseIndent();
                result += GeneratorCommon.AddLine("}") + "\n";
            }

            return result;
        }

        private static string GenerateMethod(DojoFunction m)
        {
            string result = "";

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
                        if (p.IsOptional || p.Types.Contains("null"))
                        {
                            name += "?";
                        }
                        if (!parameters.ContainsKey(name))
                        {
                            parameters[name] = new List<string>();
                        }
                        if (t != null && t.Trim() != "null")
                        {
                            parameters[name].Add(GeneratorCommon.SlashesToPeriods(t));
                        }
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
                    if (t == null || t.Trim() == "null")
                    {
                        t = "void";
                    }
                    else if (t.Trim() == "undefined")
                    {
                        return;
                    }
                    else if (t.Trim() == "function")
                    {
                        t = "Function";
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
                var signature = functionSignature.Replace("{0}", al).Replace("{1}", returnTypes.Count > 1 ? "any" : returnTypes.First());
                //HACK: correct generated signature for dojo/Stateful's watch method to align with the rest of the documentation
                if (signature.Contains("watch(name: String, callback: Function): any;"))
                {
                    signature = signature.Replace("watch(name: String, callback: Function): any;", "watch(property: string, callback:{(property?:string, oldValue?:any, newValue?: any):void}) :{unwatch():void};");
                }

                result += signature;
            });


            return result;
        }

        private static string GenerateProperties(List<DojoVariable> properties, bool isStateful)
        {
            string result = "";

            if (properties != null)
            {
                properties.ForEach(p =>
                {
                    result += GeneratorCommon.AddComment(p.Description);
                    string type = p.Types.First();
                    result += GeneratorCommon.AddLine("\"" + p.Name + "\": " + type + ";");
                    if (isStateful)
                    {
                        result += GeneratorCommon.AddLine("set(property:\"" + p.Name + "\", value: " + type + "): void;");
                        result += GeneratorCommon.AddLine("get(property:\"" + p.Name + "\"): " + type + ";");
                        result += GeneratorCommon.AddLine("watch(property:\"" + p.Name + "\", callback:{(property?:string, oldValue?:" + type + ", newValue?: " + type + "):void}) :{unwatch():void}");
                    }
                });
            }

            return result;
        }

        private static string GenerateConstructor(DojoClass obj)
        {
            string result = "";

            if (obj.Usage != null)
            {
                string value = "constructor(";
                for (var i = 0; i < obj.Usage.Parameters.Count(); i++)
                {
                    var p = obj.Usage.Parameters[i];
                    var name = p.Name;
                    if (p.IsOptional || p.Types.Contains("null"))
                    {
                        name += "?";
                    }
                    if (i > 0)
                    {
                        value += ", ";
                    }
                    value += name + ": " + GeneratorCommon.SlashesToPeriods(p.Types.First());
                }
                value += ");";
                result += GeneratorCommon.AddLine(value);
            }

            return result;
        }
        public static string WriteModuleDeclares(DojoClass obj)
        {
            var result = "";

            result += GeneratorCommon.AddLine("declare module \"" + obj.Name + "\" {");
            GeneratorCommon.IncreaseIndent();
            result += GeneratorCommon.AddLine("var exp: " + obj.Name.Replace("/", ".").Replace("-", "_"));
            result += GeneratorCommon.AddLine("export=exp;");
            GeneratorCommon.DecreaseIndent();
            result += GeneratorCommon.AddLine("}");

            obj.ChildClasses.ForEach(c => result += ClassGenerator.WriteModuleDeclares(c));
            obj.ChildObjects.ForEach(o => result += ObjectGenerator.WriteModuleDeclares(o));
            

            return result;
        }
    }
}