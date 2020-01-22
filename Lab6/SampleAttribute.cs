using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SampleAttribute : Attribute
    {
        public string Description { get; set; }

        public SampleAttribute() { }
        public SampleAttribute(string DescriptionParam)
        {
            Description = DescriptionParam;
        }
    }
}