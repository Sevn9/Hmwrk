using System;

namespace Calculator
{
  class Program
  {
    static void Main()
    {
      try
      {
        ClassCalculations calculator = new ClassCalculations();
        //string str = Console.ReadLine();

        //double value = calculator.Calculation(str);

        // Console.WriteLine(value);
        double a = GetNum();
        char c = GetOper();
        double b = GetNum();
        while (true)
        {
          a = calculator.Calculation(a, c, b);
          Console.WriteLine("Answer = \n" + a);

        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
    private static char GetOper()
    {
      return char.Parse(Console.ReadLine());
    }

    private static int GetNum()
    {
      return int.Parse(Console.ReadLine());
    }
  }
}
