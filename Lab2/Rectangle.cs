using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Rectangle : Shape, IPrint
    {
        double height;
        double width;
        
        /// <param name="ph">height</param>
        /// <param name="pw">width</param>
        public Rectangle(double ph, double pw)
        {
            this.height = ph;
            this.width = pw;
            this.Type = "Rectangle shape";
        }

        /// <summary> Area calculation </summary>
        public override double Area()
        {
            double result = this.width * this.height;
            return result;
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
