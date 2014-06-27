using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DojoTypeDescriptor.Forms;
using DojoTypeDescriptor.Models;

namespace DojoTypeDescriptor.Converters
{
    class DojoVariableConverter
    {
        public static DojoVariable Convert(DojoObjectForm obj)
        {
            var result = new DojoVariable()
            {
                IsOptional = obj.IsOptional,
                Name = obj.Name,
                Description = obj.Description,
                Types = obj.Types.ToList<string>()
            };

            return result;
        }
    }
}
