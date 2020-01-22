using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2;
using Containers;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle(8, 5);
            Square square = new Square(6.5);
            Circle circle = new Circle(3);


            ArrayList array = new ArrayList
            {
                rect,
                square,
                circle
            };

            Console.WriteLine(" -- ARRAYLIST -- ");
            Console.WriteLine("before comparison:");
            foreach (Shape x in array) x.Print();
            array.Sort();

            Console.WriteLine("\nafter comparison:");
            foreach (Shape x in array) x.Print();
            Console.WriteLine("\n");
            

            List<Shape> list = new List<Shape>
            {
                square,
                circle,
                rect
            };

            Console.WriteLine(" -- LIST -- ");
            Console.WriteLine("before comparison:");
            foreach (Shape x in list) x.Print();
            list.Sort();

            Console.WriteLine("\nafter comparison:");
            foreach (Shape x in list) x.Print();
            Console.WriteLine("\n");


            Console.WriteLine(" -- MATRIX -- ");
            Matrix3D<Shape> cube = new Matrix3D<Shape>(3, 3, 3, null);
            cube[0, 0, 0] = rect;
            cube[1, 1, 1] = circle;
            cube[2, 2, 2] = square;
            Console.WriteLine(cube.ToString());
            

            Console.WriteLine(" -- STACK -- ");
            SimpleStack<Shape> simpleStack = new SimpleStack<Shape>();
            simpleStack.Push(rect);
            simpleStack.Push(square);
            simpleStack.Push(circle);

            while (simpleStack.Count > 0)
            {
                Shape shape = simpleStack.Pop();
                Console.WriteLine(shape);
            }

            Console.Write("\nPress any button...");
            Console.ReadKey();
        }
    }
}
