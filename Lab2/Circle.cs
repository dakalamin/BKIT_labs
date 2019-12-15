using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Circle : Shape, IPrint 
    {
        double radius;

        /// <param name="pr"> radius </param>
        public Circle(double pr)
        {
            this.radius = pr;
            this.Type = "Cicle shape";
        }

        public override double Area()
        {
            double result = Math.PI * this.radius * this.radius;
            return result;
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
