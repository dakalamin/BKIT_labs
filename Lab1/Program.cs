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
            Console.ForegroundColor = ConsoleColor.Green;

            string[] input = args;

            int lenRead = input.Length;

            bool argsFound = (lenRead > 0);
            bool test = false;

            double A, B, C;
            A = B = C = 0;

            do
            {
                if (!argsFound)
                {
                    Console.WriteLine("(TIP: divide numbers with space, fraction parts with comma)");
                    Console.Write("Input A, B and C koeffs >> ");

                    input = (Console.ReadLine()).Split(' ');
                    lenRead = input.Length;
                    //foreach (string str in input) Console.WriteLine(str);

                    if (lenRead == 1 && (input[0] == "c" || input[0] == "C"))
                    {
                        Console.WriteLine("Exiting the program...");

                        Console.ReadKey();
                        return 0;
                    }
                }
                else argsFound = false;

                
                if (lenRead > 3)
                {
                    Console.WriteLine("Found more than 3 input values... Cut to 3 first.");
                }
                else if (lenRead < 3)
                {
                    Console.WriteLine("Found less than 3 input values...");
                    Console.WriteLine("Please try again (TIP: type 'c' or 'C' to exit).\n");
                    
                    continue;
                }

                test = double.TryParse(input[0], out A);
                test &= double.TryParse(input[1], out B);
                test &= double.TryParse(input[2], out C);

                if (!test)
                {
                    Console.WriteLine("Incorrect input...");
                    Console.WriteLine("Please try again (TIP: type 'c' or 'C' to exit).\n");
                }
            } while (!test);
            
            if (A == 0)
            {
                if (B == 0)
                {
                    Console.WriteLine("(not actually an equation)");
                    Console.WriteLine("x - is any real number");
                }
                else
                {
                    double res = -C / B;

                    Console.WriteLine("(not actually a quadratic equation)");
                    Console.Write("x = ");
                    Console.WriteLine(res);
                }
            }
            else
            {
                double discr = B * B - 4 * A * C;
                if (discr > 0)
                {
                    double res1 = (-B + Math.Sqrt(discr)) / (2 * A);
                    double res2 = (-B - Math.Sqrt(discr)) / (2 * A);

                    Console.Write("x1 = ");
                    Console.WriteLine(res1);
                    Console.Write("x2 = ");
                    Console.WriteLine(res2);

                }
                else if (discr == 0)
                {
                    double res = -B / (2 * A);

                    Console.Write("x = ");
                    Console.WriteLine(res);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No real solutions");
                }
            }
            
            Console.ReadKey();
            return 0;
        }
    }
}
