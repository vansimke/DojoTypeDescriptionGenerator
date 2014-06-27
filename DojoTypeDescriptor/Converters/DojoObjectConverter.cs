using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DojoTypeDescriptor.Forms;
using DojoTypeDescriptor.Models;

namespace DojoTypeDescriptor.Converters
{
    class DojoObjectConverter
    {
        public static DojoObject Convert(DojoObjectForm obj)
        {
            var result = new DojoObject()
            {
                Permalink = obj.Permalink,
                Name = obj.Name,
                Description = obj.Description
            };
            obj.Properties.ForEach(property =>
            {
                result.Properties.Add(DojoVariableConverter.Convert(property));
            });
            obj.Methods.ForEach(method =>
            {
                result.Methods.Add(DojoFunctionConverter.Convert(method));
            });

            if (obj.Children != null)
            {
                obj.Children.ForEach(child =>
                {
                    switch (child.Type)
                    {
                        case "DojoObject":
                            result.ChildObjects.Add(DojoObjectConverter.Convert(child));
                            break;
                        case "DojoConstructor":
                            result.ChildClasses.Add(DojoClassConverter.Convert(child));
                            break;
                        case "DojoPackage":
                            result.ChildPackages.Add(DojoPackageConverter.Convert(child));
                            break;
                        case "DojoInstance":
                            result.ChildInstances.Add(DojoInstanceConverter.Convert(child));
                            break;
                        case "DojoFunction":
                            result.ChildFunctions.Add(DojoFunctionConverter.Convert(child));
                            break;
                        default:
                            throw new Exception(String.Format("Unable to add {0} [{1}] to Object {2}", child.Name, child.Type, obj.Name));
                    }
                });
            }
            

            return result;
        }
    }
}
