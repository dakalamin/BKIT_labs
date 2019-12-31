using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Square : Rectangle
    {
        /// <param name="length"> square length (strictly positive)</param>
        public Square(double length) : base(length, length, "Square") { }
    }
}
