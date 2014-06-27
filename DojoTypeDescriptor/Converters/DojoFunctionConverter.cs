using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DojoTypeDescriptor.Forms;
using DojoTypeDescriptor.Models;

namespace DojoTypeDescriptor.Converters
{
    class DojoFunctionConverter
    {
        public static DojoFunction Convert(DojoObjectForm obj)
        {
            if (obj == null)
            {
                return null;
            }

            var result = new DojoFunction()
            {
                Permalink = obj.Permalink,
                Name = obj.Name,
                Description = obj.Description,
                Usage = DojoFunctionConverter.Convert(obj.Usage),
            };

            obj.Properties.ForEach(property =>
            {
                result.Properties.Add(DojoVariableConverter.Convert(property));
            });
            obj.Methods.ForEach(method =>
            {
                result.Methods.Add(Convert(method));
            });
            obj.ReturnTypes.ForEach(returnType =>
            {
                result.ReturnTypes.Add(DojoVariableConverter.Convert(returnType));
            });
            obj.Parameters.ForEach(parameter =>
            {
                result.Parameters.Add(DojoVariableConverter.Convert(parameter));
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
                        case "DojoFunction":
                            result.ChildFunctions.Add(DojoFunctionConverter.Convert(child));
                            break;
                        default:
                            throw new Exception(String.Format("Unable to add {0} [{1}] to Class {2}", child.Name, child.Type, obj.Name));
                    }
                });
            }

            return result;
        }
    }
}
