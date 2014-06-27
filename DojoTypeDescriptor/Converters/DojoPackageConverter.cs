using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DojoTypeDescriptor.Forms;
using DojoTypeDescriptor.Models;

namespace DojoTypeDescriptor.Converters
{
    public class DojoPackageConverter
    {
        public static DojoPackage Convert(DojoObjectForm input)
        {
            var result = new DojoPackage();

            result.Name = input.Name;

            if (input.Children != null)
            {
                input.Children.ForEach(child =>
                {
                    switch (child.Type)
                    {
                        case "DojoPackage":
                            result.Packages.Add(Convert(child));
                            break;
                        case "DojoConstructor":
                            result.Classes.Add(DojoClassConverter.Convert(child));
                            break;
                        case "DojoFunction":
                            result.Functions.Add(DojoFunctionConverter.Convert(child));
                            break;
                        case "DojoObject":
                            result.Objects.Add(DojoObjectConverter.Convert(child));
                            break;
                        case "DojoVariable":
                            throw new Exception("Unable to assign a variable '" + child.Name + "' to package '" + input.Name + "'");
                    }
                });
            }

            return result;
        }
    }
}