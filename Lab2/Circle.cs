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

        public override void Print() => Print(Math.Round(Area, 2));
    }
}
