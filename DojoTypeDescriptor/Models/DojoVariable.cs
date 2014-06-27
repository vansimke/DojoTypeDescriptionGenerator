using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DojoTypeDescriptor.Models
{
    public class DojoVariable
    {
        public DojoVariable()
        {
            Types = new List<string>();
        }

        public bool IsOptional { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Types { get; set; }
    }
}