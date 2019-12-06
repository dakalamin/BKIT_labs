using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        public static ConsoleColor DefaultColor = ConsoleColor.White;
        public static ConsoleColor OkColor = ConsoleColor.Green;
        public static ConsoleColor BadColor = ConsoleColor.Red;

        // simple cnsl output with chosen color, then reset to prev
        static void DisplayClrd(string output, ConsoleColor pushedColor, bool forceDflt=false)
        {
            Console.ForegroundColor = pushedColor;
            Console.WriteLine(output);

            Console.ForegroundColor = DefaultColor;
        }

        static bool Solution(double res, ref int xNum)
        {
            bool solutionFound = true;

            if (res > 0)
            {
                res = Math.Sqrt(res);
                DisplayClrd($"x{ xNum } = { res}\n" + 
                           $"x{xNum+1} = {-res}",
                           OkColor);

                xNum += 2;
            }
            else if (res == 0)
            {
                if (xNum != 2)
                {
                    DisplayClrd($"x{ xNum } = 0",
                               OkColor);
                }
                xNum += 1;
            }
            else solutionFound = false;

            return solutionFound;
        }

        static int Main(string[] args)
        {
            Console.Title = "Daniil Kalamin -- IU5-34";
            Console.ForegroundColor = DefaultColor;

            string[] input = args;

            int lenRead = input.Length;

            bool argsFound = (lenRead > 0);
            bool test = false;

            double A, B, C;
            A = B = C = 0;

            do
            {
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
                    DisplayClrd("Found less than 3 input values...\n" + 
                               "Please try again (TIP: type 'c' or 'C' to exit).\n",
                               BadColor);

                    continue;
                }

                // returns TRUE if all parsed successfully
                test = double.TryParse(input[0], out A);
                test &= double.TryParse(input[1], out B);
                test &= double.TryParse(input[2], out C);

                if (!test)
                {
                    DisplayClrd("Incorrect input...\n" +
                               "Please try again (TIP: type 'c' or 'C' to exit).\n",
                               BadColor);
                }
            } while (!test);
            
            double discr = 0;
            int ind = 1; // DO NOT SHANGE - initial xNum val for Solution()

            bool solutionFound = true;
            if (A == 0)
            {
                if (B == 0)
                {
                    Console.WriteLine("(not actually an equation)");
                    if (C == 0)
                    {
                        DisplayClrd("x - is any real number",
                                   OkColor);
                    }
                    else solutionFound = false;
                }
                else
                {
                    // solving quadratic equation
                    Console.WriteLine("(actually a quadratic equation)");
                    solutionFound = Solution(-C, ref ind);
                }
            }
            else
            {
                // solving biquadratic equation
                discr = B * B - 4 * A * C;
                if (discr > 0)
                {
                    bool localSF1 = Solution((-B + Math.Sqrt(discr)) / (2 * A), ref ind);
                    bool localSF2 = Solution((-B - Math.Sqrt(discr)) / (2 * A), ref ind);

                    solutionFound = (localSF1 || localSF2);
                }
                else if (discr == 0)
                {
                    solutionFound = Solution(-B / (2 * A), ref ind);
                }
                else solutionFound = false;
            }

            if (!solutionFound) DisplayClrd("No real solutions", BadColor);

            if (A != 0)
            {
                // discr show question
                Console.Write("\nDo you want to output discriminant? (y/any other symb)>> ");

                char answer = (char)Console.Read();
                if (answer == 'y' || answer == 'Y')
                {
                    Console.WriteLine($"D = {discr}");
                }
            }

            Console.Write("\nPress any button...");
            Console.ReadKey();
            return 0;
        }
    }
}
