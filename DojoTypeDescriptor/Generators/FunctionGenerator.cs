using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DojoTypeDescriptor.Models;

namespace DojoTypeDescriptor.Generators
{
    public class FunctionGenerator
    {
        public static string Generate(DojoFunction obj)
        {
            string result = "";
            result += GenerateUsage(obj);

            if (obj.Properties.Count() > 0 || obj.Methods.Count() > 0 || obj.ChildClasses.Count() > 0 || obj.ChildFunctions.Count > 0 || obj.ChildObjects.Count > 0)
            {
                result += GeneratorCommon.AddLine("module " + GeneratorCommon.GetShortName(obj.Name) + " {");

                GeneratorCommon.IncreaseIndent();
                result += GeneratorCommon.GenerateProperties(obj.Properties);
                obj.Methods.ForEach(m => result += FunctionGenerator.Generate(m));
                obj.ChildFunctions.ForEach(cf => result += FunctionGenerator.Generate(cf));
                obj.ChildClasses.ForEach(cc => result += ClassGenerator.Generate(cc));
                obj.ChildObjects.ForEach(co => result += ObjectGenerator.Generate(co));

                GeneratorCommon.DecreaseIndent();
                result += GeneratorCommon.AddLine("}") + "\n";
            }

            return result;
        }

        private static string GenerateUsage(DojoFunction obj)
        {
            string result = "";

            DojoFunction parameterSource = obj.Usage != null ? obj.Usage : obj;

            string comment = GeneratorCommon.AddComment(obj.Description, parameterSource.Parameters, obj.Permalink);
            string functionSignature = "";
            functionSignature += GeneratorCommon.AddLine("interface " + GeneratorCommon.GetShortName(obj.Name) + "{({0}): {1}}");



            var parameters = new Dictionary<string, List<string>>();
            var argumentLists = new List<string>();
            argumentLists.Add("");
            if (parameterSource.Parameters != null && parameterSource.Parameters.Count() > 0)
            {
                parameterSource.Parameters.ForEach(p =>
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
                        parameters[name].Add(GeneratorCommon.SlashesToPeriods(t == null ? "void" : t));
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
                            string newParameter = parameterName;
                            newArgList += argList.Length > 0 ? ", " : "";
                            if (argList.Contains("?") && !newParameter.Contains("?"))
                            {
                                newParameter += "?";
                            }
                            newArgList += newParameter + ": " + t;
                            newArgumentLists.Add(newArgList);
                        });

                    });
                    argumentLists = newArgumentLists;

                }
            }

            var returnTypes = new List<string>();
            if (parameterSource.ReturnTypes != null && parameterSource.ReturnTypes.Count() > 0)
            {

                parameterSource.ReturnTypes.ForEach(rt => rt.Types.ForEach(t =>
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
                result += functionSignature.Replace("{0}", al).Replace("{1}", returnTypes.Count > 1 ? "any" : returnTypes.First());
            });


            return result;
        }
    }
}