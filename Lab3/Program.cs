using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle(8, 5);
            Square  square = new Square(6.5);
            Circle  circle = new Circle(3);

            ArrayList array = new ArrayList();
            array.Add(rect);
            array.Add(square);
            array.Add(circle);

            Console.WriteLine(" -- ARRAYLIST -- ");
            Console.WriteLine("before comparison:");
            foreach (var x in array) Console.WriteLine(x);
            array.Sort();

            Console.WriteLine("\nafter comparison:");
            foreach (var x in array) Console.WriteLine(x);
            Console.WriteLine("\n");


            List<Shape> list = new List<Shape>();
            list.Add(square);
            list.Add(circle);
            list.Add(rect);

            Console.WriteLine(" -- LIST<> -- ");
            Console.WriteLine("before comparison:");
            foreach (var x in list) Console.WriteLine(x);
            list.Sort();

            Console.WriteLine("\nafter comparison:");
            foreach (var x in list) Console.WriteLine(x);

            Console.ReadKey();
        }
    }
}
