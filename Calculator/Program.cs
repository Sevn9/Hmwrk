using System;
using System.Diagnostics.CodeAnalysis;

namespace Calculator
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main()
        {
            try
            {
                Calculator calculator = new Calculator();
                string str = Console.ReadLine();

                double value = calculator.Calculation(str);

                Console.WriteLine(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
