using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DojoTypeDescriptor.Models
{
    public class DojoFunction
    {
        public DojoFunction()
        {
            Properties = new List<DojoVariable>();
            Methods = new List<DojoFunction>();
            ReturnTypes = new List<DojoVariable>();
            Parameters = new List<DojoVariable>();
            ChildObjects = new List<DojoObject>();
            ChildClasses = new List<DojoClass>();
            ChildFunctions = new List<DojoFunction>();
        }

        public string Permalink { get; set; }
        public string Name { get; set; }
        public string  Description { get; set; }
        public DojoFunction Usage { get; set; }
        public List<DojoVariable> Properties { get; set; }
        public List<DojoFunction> Methods { get; set; }
        public List<DojoVariable> ReturnTypes { get; set; }
        public List<DojoVariable> Parameters { get; set; }

        public List<DojoObject> ChildObjects { get; set; }
        public List<DojoClass> ChildClasses { get; set; }
        public List<DojoFunction> ChildFunctions { get; set; }

        
    }
}