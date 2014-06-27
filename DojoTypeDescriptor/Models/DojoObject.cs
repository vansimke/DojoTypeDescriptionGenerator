using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DojoTypeDescriptor.Models
{
    public class DojoObject
    {
        public DojoObject()
        {
            Properties = new List<DojoVariable>();
            Methods = new List<DojoFunction>();
            ChildObjects = new List<DojoObject>();
            ChildClasses = new List<DojoClass>();
            ChildPackages = new List<DojoPackage>();
            ChildInstances = new List<DojoInstance>();
            ChildFunctions = new List<DojoFunction>();
        }

        public string Permalink { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DojoVariable> Properties { get; set; }
        public List<DojoFunction> Methods { get; set; }

        public List<DojoObject> ChildObjects { get; set; }
        public List<DojoClass> ChildClasses { get; set; }
        public List<DojoPackage> ChildPackages { get; set; }
        public List<DojoInstance> ChildInstances { get; set; }
        public List<DojoFunction> ChildFunctions { get; set; }
    }
}