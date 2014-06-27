using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DojoTypeDescriptor.Forms;

namespace DojoTypeDescriptor.Helpers
{
    public class DojoObjectHelper
    {
        public static DojoObjectForm MergeChildren(DojoObjectForm form)
        {

            var result = form.Copy();
            var children = new Dictionary<string, DojoObjectForm>();

            form.Children.ForEach(child =>
            {
                if (children.ContainsKey(child.Name) && children[child.Name] != child)
                {
                    children[child.Name] = Merge(children[child.Name], child);
                }
                else
                {
                    children[child.Name] = child;
                }
            });

            result.Children = children.Values.ToList<DojoObjectForm>();


            var mergedChildren = new List<DojoObjectForm>();
            result.Children.ForEach(child => mergedChildren.Add(MergeChildren(child)));

            result.Children = mergedChildren;
            return result;
        }

        private static DojoObjectForm Merge(DojoObjectForm l, DojoObjectForm r)
        {
            var result = new DojoObjectForm();
            DojoObjectForm parent = null;
            DojoObjectForm mixin = null;

            if (!l.Type.Equals("DojoPackage") && !r.Type.Equals("DojoPackage"))
            {
                if (r.Type.Equals("DojoConstructor") || r.Type.Equals("DojoFunction"))
                {
                    return r;
                }
                else
                {
                    return l;
                }
            }
            else if (!l.Type.Equals("DojoPackage"))
            {
                parent = l;
                mixin = r;
            }
            else
            {
                parent = r;
                mixin = l;
            }

            result.Permalink = parent.Permalink;
            result.Name = parent.Name;
            result.IsOptional = parent.IsOptional;
            result.BaseClass = parent.BaseClass;
            result.Description = parent.Description;
            result.Type = parent.Type;
            result.Children = parent.Children.ToList<DojoObjectForm>().Concat(mixin.Children.ToList<DojoObjectForm>()).ToList<DojoObjectForm>();
            result.Events = parent.Events.ToList<DojoObjectForm>().Concat(mixin.Events.ToList<DojoObjectForm>()).ToList<DojoObjectForm>();
            result.Methods = parent.Methods.ToList<DojoObjectForm>().Concat(mixin.Methods.ToList<DojoObjectForm>()).ToList<DojoObjectForm>();
            result.Mixins = parent.Mixins.ToList<string>();
            result.Parameters = parent.Parameters.ToList<DojoObjectForm>().Concat(mixin.Parameters.ToList<DojoObjectForm>()).ToList<DojoObjectForm>();
            result.Properties = parent.Properties.ToList<DojoObjectForm>().Concat(mixin.Properties.ToList<DojoObjectForm>()).ToList<DojoObjectForm>();
            result.ReturnTypes = parent.ReturnTypes.ToList<DojoObjectForm>().Concat(mixin.ReturnTypes.ToList<DojoObjectForm>()).ToList<DojoObjectForm>();
            result.Types = parent.Types.ToList<string>();
            result.Usage = parent.Usage;

            for (var i = result.Properties.Count - 1; i >= 0; i--)
            {
                if (result.Children.Any(c => c.Name.Equals(result.Properties[i].Name)))
                {
                    result.Properties.Remove(result.Properties[i]);
                }
            }

            return result;
        }

        public static void BuildParentTree(DojoObjectForm obj, DojoObjectForm topLevel)
        {

            if (obj.Type.Equals("DojoConstructor"))
            {
                if (obj.BaseClass != null)
                {
                    DojoObjectForm baseClass = FindObject(obj.BaseClass, topLevel);
                    if (baseClass != null && baseClass.AncestorTypes.Count() == 0)
                    {
                        BuildParentTree(baseClass, topLevel);
                    }
                }
                if (obj.Mixins != null)
                {
                    obj.Mixins.ForEach(m =>
                    {
                        DojoObjectForm mixin = FindObject(m, topLevel);
                        if (mixin != null && mixin.AncestorTypes != null)
                        {
                            if (mixin.AncestorTypes.Count() == 0)
                            {
                                BuildParentTree(mixin, topLevel);
                            }
                        }
                    });
                }

                if (obj.BaseClass != null)
                {
                    DojoObjectForm baseClass = FindObject(obj.BaseClass, topLevel);
                    if (baseClass != null)
                    {
                        obj.AncestorTypes.Add(baseClass.Name);
                        obj.AncestorTypes = obj.AncestorTypes.Concat(baseClass.AncestorTypes).ToList<string>();
                    }
                }
                if (obj.Mixins != null)
                {
                    obj.Mixins.ForEach(m =>
                    {
                        DojoObjectForm mixin = FindObject(m, topLevel);
                        if (mixin != null)
                        {
                            obj.AncestorTypes.Add(mixin.Name);
                            obj.AncestorTypes = obj.AncestorTypes.Concat(mixin.AncestorTypes).ToList<string>();
                        }
                    });
                }
            }

            if (obj.Children != null && obj.Children.Count() > 0)
            {
                obj.Children.ForEach(child => BuildParentTree(child, topLevel));
            }
        }

        private static DojoObjectForm FindObject(string name, DojoObjectForm parent)
        {
            DojoObjectForm result = null;

            if (parent.Name != null && parent.Name.Equals(name))
            {
                result = parent;
            }
            else if (parent.Children == null || parent.Children.Count() == 0)
            {
                return null;
            }
            else
            {
                var results = parent.Children.Select(child => FindObject(name, child));
                result = results.FirstOrDefault(r => r != null);
            }

            return result;
        }
    }
}