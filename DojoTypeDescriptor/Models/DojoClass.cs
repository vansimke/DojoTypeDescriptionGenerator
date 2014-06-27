using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DojoTypeDescriptor.Models
{
    public class DojoClass
    {
        public DojoClass()
        {
            Properties = new List<DojoVariable>();
            Methods = new List<DojoFunction>();
            Events = new List<DojoFunction>();
            ChildObjects = new List<DojoObject>();
            ChildClasses = new List<DojoClass>();
            Mixins = new List<string>();
            Ancestors = new List<string>();
        }

        public string Permalink { get; set; }
        public string Name { get; set; }
        public string BaseClass { get; set; }
        public List<string> Mixins { get; set; }
        public string Description { get; set; }
        public DojoFunction Usage { get; set; }
        public List<DojoVariable> Properties { get; set; }
        public List<DojoFunction> Methods { get; set; }
        public List<DojoFunction> Events { get; set; }
        public List<string> Ancestors { get; set; }

        public List<DojoObject> ChildObjects { get; set; }
        public List<DojoClass> ChildClasses { get; set; }
    }
}
