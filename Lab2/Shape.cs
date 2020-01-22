using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public abstract class Shape : IComparable, IPrint
    {
        public readonly string Type;

        public Shape(string type)
        {
            Type = type;
        }

        /// <summary> Shape AREA property </summary>
        public abstract double Area { get; }

        /// <summary> Setting to string </summary>
        protected string ToString(double data) => $"{Type}: {data}";
        public override string ToString() => this.ToString(Area);

        /// <summary> Console output </summary>
        protected void Print(double data) => Console.WriteLine($"{Type} shape covers an area of {data}");
        public virtual void Print() => this.Print(Area);

        /// <summary> Filter - pass only positive vals </summary>
        protected void Parse(double value, out double property)
        {
            if (value <= 0) throw new Exception(nameof(property));
            property = value;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            double objArea = ((Shape)obj).Area;

            if      (this.Area < objArea) return -1;
            else if (this.Area > objArea) return  1;
            else return 0;
        }
    }
}
