using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Rectangle : Shape
    {
        private double height;
        public double Height
        {
            get => height;
            protected set => Parse(value, out height);
        }

        private double width;
        public double Width
        {
            get => width;
            protected set => Parse(value, out width);
        }

        protected Rectangle(double height, double width, string type) : base(type)
        {
            this.Height = height;
            this.Width = width;
        }

        /// <param name="height"> Rectangle height (strictly positive)</param>
        /// <param name="width">  Rectangle width  (strictly positive)</param>
        public Rectangle(double height, double width) : this(height, width, "Rectangle") { }

        /// <summary> Area calculation </summary>
        public override double Area => Width * Height;
    }
}
