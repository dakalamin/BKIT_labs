using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{

    class Program
    {
        static int Main(string[] args)
        {
            Console.Title = "Daniil Kalamin -- IU5-34";

            bool test = double.TryParse(args[0], out double A);
            test &= double.TryParse(args[1], out double B);
            test &= double.TryParse(args[2], out double C);

            if (!test)
            {
                Console.WriteLine("Please enter A, B and C koeffs.");
                return 1;
            }

            double discr = B*B - 4*A*C;
            if (discr > 0)
            {
                double res1 = (-B + Math.Sqrt(discr)) / (2*A);
                double res2 = (-B - Math.Sqrt(discr)) / (2*A);

                Console.Write("x1 = ");
                Console.WriteLine(res1);
                Console.Write("x2 = ");
                Console.WriteLine(res2);

            }
            else if (discr == 0)
            {
                double res = -B/(2*A);

                Console.Write("x = ");
                Console.WriteLine(res);
            }
            else
            {
                Console.WriteLine("No real solutions");
            }

            Console.ReadKey();
            return 0;
        }
    }
}
