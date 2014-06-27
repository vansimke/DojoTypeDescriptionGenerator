using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DojoTypeDescriptor.Models
{
    public class DojoInstance
    {
        public DojoInstance()
        {
            Properties = new List<DojoVariable>();
            Methods = new List<DojoFunction>();
            Events = new List<DojoFunction>();
            Mixins = new List<string>();
            ChildObjects = new List<DojoObject>();
        }


        public string Permalink { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; }
        public DojoFunction Usage { get; set; }
        public string BaseClass { get; set; }
        public List<string> Mixins { get; set; }
        public List<DojoVariable> Properties { get; set; }
        public List<DojoFunction> Methods { get; set; }
        public List<DojoFunction> Events { get; set; }

        public List<DojoObject> ChildObjects { get; set; }
    }
}
