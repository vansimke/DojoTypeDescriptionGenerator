using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DojoTypeDescriptor.Models;

namespace DojoTypeDescriptor.Generators
{
    public class GeneratorCommon
    {
        private static int indentLevel = 0;
        private const int INDENT_SPACES = 4;

        public static void IncreaseIndent() {
            indentLevel++;
        }

        public static void DecreaseIndent() {
            indentLevel--;
        }

        public static int GetIndentLevel()
        {
            return indentLevel;
        }

        public static string AddIndents()
        {
            return "".PadLeft(INDENT_SPACES * indentLevel);
        }

        public static string AddLine(string line)
        {
            return AddIndents() + line + "\n";
        }

        public static string AddComment(string comment, List<DojoVariable> arguments = null, string permalink = null)
        {
            string result = "";
            comment = comment == null ? "" : comment.Replace("/*", "//").Replace("*/", "//");

            result += AddLine("/**");
            
            if (permalink != null)
            {
                result += AddLine(" * Permalink: http://dojotoolkit.org" + permalink);
                result += AddLine(" *");
            }
            result += AddLine(" * " + comment.Replace("\n\n", "\n").Replace("\n", "\n" + AddIndents() + " * "));

            if (arguments != null)
            {
                arguments.ForEach(a => result += AddLine(" * @param " + a.Name + " " + a.Description.Replace("\n", "").Replace("/*", "//").Replace("*/", "//")));
            }

            result += AddLine(" */");

            return result;
        }

        public static string GetShortName(string name)
        {
            var result = "";
            if (name.Contains("/") || name.Contains("."))
            {
                result = name.Substring(name.LastIndexOfAny(new char[] { '/', '.' }) + 1);
            }
            else
            {
                result = name;
            }

            result = result.Replace('-', '_');

            result = EscapeKeywords(result);


            return result;
        }

        public static string SlashesToPeriods(string value)
        {
            if (value == null)
            {
                value = "void";
            }
            return value.Replace("/", ".");
        }

        public static string EscapeKeywords(string value)
        {
            var keywords = new List<string>(){
                               "default",
                               "string",
                               "number",
                               "in"
                           };

            string result = value;

            if (keywords.Contains(value))
            {
                result += "_";
            }

            return result;

        }

        public static string EscapeKeywordsInModuleDeclarations(string elementName)
        {
            var keywords = new List<string>(){
                               "default",
                               "string",
                               "number"                               
                           };

            string result = elementName;

            foreach (var keyword in keywords)
            {
                result = result.Replace(keyword, keyword + "_");
            }

            return result;

        }

        public static string GenerateProperties(List<DojoVariable> properties)
        {
            string result = "";

            if (properties != null)
            {
                properties.ForEach(p =>
                {
                    result += AddComment(p.Description);
                    result += AddLine(EscapeKeywords(p.Name) + ": " + p.Types.First() + ";");
                });
            }

            return result;  
        }

        public static string GenerateMethods(List<DojoVariable> list)
        {
            string result = "";

            return result;
        }
    }
}