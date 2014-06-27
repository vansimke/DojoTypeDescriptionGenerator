using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DojoTypeDescriptor.Models
{
    public class DojoPackage
    {

        public DojoPackage()
        {
            Packages = new List<DojoPackage>();
            Functions = new List<DojoFunction>();
            Objects = new List<DojoObject>();
            Classes = new List<DojoClass>();
            Singletons = new List<DojoInstance>();
        }

        public string Name { get; set; }
        public List<DojoPackage> Packages { get; set; }
        public List<DojoFunction> Functions { get; set; }
        public List<DojoObject> Objects { get; set; }
        public List<DojoClass> Classes { get; set; }
        public List<DojoInstance> Singletons { get; set; }
    }
}