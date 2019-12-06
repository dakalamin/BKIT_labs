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
            ConsoleColor defaultColor = ConsoleColor.White;
            ConsoleColor okColor = ConsoleColor.Green;
            ConsoleColor badColor = ConsoleColor.Red;

            Console.Title = "Daniil Kalamin -- IU5-34";

            string[] input = args;

            int lenRead = input.Length;

            bool argsFound = (lenRead > 0);
            bool test = false;

            double A, B, C;
            A = B = C = 0;

            do
            {
                Console.ForegroundColor = defaultColor;

                /* repeated input attemps 
                 * (first - if 'args' input was incorrect,
                 *   than - if it itself was again incorrect)    */
                if (!argsFound)
                {
                    Console.WriteLine("(TIP: divide numbers with space, fraction parts with comma)");
                    Console.Write("Input A, B and C koeffs >> ");

                    input = (Console.ReadLine()).Split(' ');
                    lenRead = input.Length;
                    
                    // a chance to exit the program
                    if (lenRead == 1 && (input[0] == "c" || input[0] == "C"))
                    {
                        Console.WriteLine("Exiting the program.");
                        Console.Write("\nPress any button...");

                        Console.ReadKey();
                        return 0;
                    }
                }
                else argsFound = false; // so the next time it will offer to repeat input

                // length (num of elemrnts read) filter
                if (lenRead > 3)
                {
                    // it's ok, just ignore all extra elements
                    Console.WriteLine("Found more than 3 input values... Cut to 3 first.");
                }
                else if (lenRead < 3)
                {
                    // not ok, no point to parse input -> straight to next 'do' iteration
                    Console.ForegroundColor = badColor;
                    Console.WriteLine("Found less than 3 input values...");
                    Console.WriteLine("Please try again (TIP: type 'c' or 'C' to exit).\n");

                    continue;
                }

                // returns TRUE if all parsed successfully
                test = double.TryParse(input[0], out A);
                test &= double.TryParse(input[1], out B);
                test &= double.TryParse(input[2], out C);

                if (!test)
                {
                    Console.ForegroundColor = badColor;
                    Console.WriteLine("Incorrect input...");
                    Console.WriteLine("Please try again (TIP: type 'c' or 'C' to exit).\n");
                }
            } while (!test);

            // green by defaul as the majority of outputs show solutions
            Console.ForegroundColor = okColor;

            double discr = 0;

            bool solutionFound = true;
            if (A == 0)
            {
                if (B == 0)
                {
                    Console.WriteLine("(not actually an equation)");

                    if (C == 0)
                    {
                        Console.WriteLine("x - is any real number");
                    }
                    else solutionFound = false;
                }
                else
                {
                    // solving quadratic equation
                    double res = -C;

                    Console.WriteLine("(actually a quadratic equation)");
                    if (res > 0)
                    { 
                        res = Math.Sqrt(res);
                        Console.WriteLine($"x1 = {res}");
                        Console.WriteLine($"x2 = -{res}");
                    }
                    else if (res == 0)
                    {
                        Console.WriteLine($"x = 0");
                    }
                    else
                    {
                        // red because no solutions
                        Console.ForegroundColor = badColor;
                        Console.WriteLine("No real solutions");
                    }
                }
            }
            else
            {
                // solving quadratic equation
                discr = B * B - 4 * A * C;
                if (discr > 0)
                {
                    int i = 1;

                    double res1 = (-B + Math.Sqrt(discr)) / (2 * A);
                    if (res1 > 0)
                    {
                        res1 = Math.Sqrt(res1);
                        i += 2;

                        Console.WriteLine($"x1 = {res1}");
                        Console.WriteLine($"x2 = -{res1}");
                    }
                    else if (res1 == 0)
                    {
                        i += 1;
                        Console.WriteLine("x1 = 0");
                    }

                    double res2 = (-B - Math.Sqrt(discr)) / (2 * A);
                    if (res2 > 0)
                    {
                        res2 = Math.Sqrt(res2);

                        Console.WriteLine($"x{i} = {res2}");
                        Console.WriteLine($"x{i + 1} = -{res2}");
                    }
                    else if (res2 == 0 && res1 != 0)
                    {
                        Console.WriteLine($"x{i} = 0");
                    }

                    solutionFound = (res1 >= 0 || res2 >= 0);
                }
                else if (discr == 0)
                {
                    double res = -B / (2 * A);
                    if (res > 0)
                    {
                        res = Math.Sqrt(res);

                        Console.WriteLine($"x1 = {res}");
                        Console.WriteLine($"x2 = -{res}");
                    }
                    else solutionFound = false;
                }
                else solutionFound = false;
            }

            if (!solutionFound)
            {
                // red because no solutions
                Console.ForegroundColor = badColor;
                Console.WriteLine("No real solutions");
            }

            // discr show question
            Console.ForegroundColor = defaultColor;
            Console.Write("\nDo you want to output discriminant? (y/any other symb)>> ");

            char answer = (char)Console.Read();
            if (answer == 'y' || answer == 'Y')
            {
                Console.WriteLine($"D = {discr}");
            }

            Console.Write("\nPress any button...");
            Console.ReadKey();
            return 0;
        }
    }
}
