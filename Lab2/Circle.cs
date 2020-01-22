using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Circle : Shape
    {
        private double radius;
        public double Radius
        {
            get => radius;
            protected set => Parse(value, out radius);
        }
        
        /// <param name="radius"> circle radius (strictly positive)</param>
        public Circle(double radius) : base("Circle")
        {
            this.Radius = radius;
        }

        public override double Area => Math.PI * Radius * Radius;
        private double areaRounded => Math.Round(Area, 2);

        public override string ToString() => this.ToString(areaRounded);
        public override void Print()      => this.Print(areaRounded);
    }
}
