using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Square : Rectangle
    {
        public Square(double size) : base(size, size)
        {
            this.Type = "Square shape";
        }
    }
}
