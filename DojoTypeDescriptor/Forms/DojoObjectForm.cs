using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DojoTypeDescriptor.Forms
{
    public class DojoObjectForm
    {

        public DojoObjectForm()
        {
            Children = new List<DojoObjectForm>();
            Properties = new List<DojoObjectForm>();
            Methods = new List<DojoObjectForm>();
            Mixins = new List<string>();
            Events = new List<DojoObjectForm>();
            ReturnTypes = new List<DojoObjectForm>();
            Parameters = new List<DojoObjectForm>();
            Types = new List<string>();
            AncestorTypes = new List<string>();

        }

        public string Type { get; set; }
        public string Permalink { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DojoObjectForm> Children { get; set; }
        public List<DojoObjectForm> Properties { get; set; }
        public List<DojoObjectForm> Methods { get; set; }
        public string BaseClass { get; set; }
        public List<string> Mixins { get; set; }
        public List<DojoObjectForm> Events { get; set; }
        public DojoObjectForm Usage { get; set; }
        public List<DojoObjectForm> ReturnTypes { get; set; }
        public List<DojoObjectForm> Parameters { get; set; }
        public bool IsOptional { get; set; }
        public List<string> Types { get; set; }
        public List<string> AncestorTypes { get; set; }

        public DojoObjectForm Copy()
        {
            var result = new DojoObjectForm()
            {
                Type = Type,
                Permalink = Permalink,
                Name = Name,
                Description =Description,
                Children = Children.ToList<DojoObjectForm>(),
                Properties = Properties.ToList<DojoObjectForm>(),
                Methods = Methods.ToList<DojoObjectForm>(),
                BaseClass = BaseClass,
                Mixins = Mixins.ToList<string>(),
                Events = Events.ToList<DojoObjectForm>(),
                Usage = Usage != null ? Usage.Copy() : null,
                ReturnTypes = ReturnTypes.ToList<DojoObjectForm>(),
                Parameters = Parameters.ToList<DojoObjectForm>(),
                IsOptional = IsOptional,
                Types = Types.ToList<string>(),
                AncestorTypes = AncestorTypes.ToList<string>()
            };

            return result;
        }


    }
}