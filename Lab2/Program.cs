﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle(5, 4);
            Square  square = new Square(5);
            Circle  circle = new Circle(5);

            Shape[] array = new Shape[3] { rect, square, circle };

            foreach (Shape obj in array) obj.Print();

            Console.Write("\nPress any button...");
            Console.ReadKey();
        }
    }
}
