using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DojoTypeDescriptor.Forms;
using DojoTypeDescriptor.Models;

namespace DojoTypeDescriptor.Converters
{
    class DojoClassConverter
    {
        public static DojoClass Convert(DojoObjectForm obj)
        {
            if (obj.Name == "dijit/form/Button")
            {
                var i = 1;
            }
            var result = new DojoClass()
            {
                Permalink = obj.Permalink,
                Name = obj.Name,
                BaseClass = obj.BaseClass,
                Mixins = obj.Mixins.ToList<string>(),
                Description = obj.Description,
                Usage = DojoFunctionConverter.Convert(obj.Usage),
                Ancestors = obj.AncestorTypes.ToList<string>()
            };

            obj.Properties.ForEach(property =>
            {
                result.Properties.Add(DojoVariableConverter.Convert(property));
            });
            obj.Methods.ForEach(method =>
            {
                result.Methods.Add(DojoFunctionConverter.Convert(method));
            });
            obj.Events.ForEach(evt =>
            {
                result.Events.Add(DojoFunctionConverter.Convert(evt));
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
                        default:
                            throw new Exception(String.Format("Unable to add {0} [{1}] to Class {2}", child.Name, child.Type, obj.Name));
                    }
                });
            }


            return result;
        }
    }
}
