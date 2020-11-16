using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    class Calculator
    {
        private double result;
        private char symbol;
        private double number1;
        private double number2;
        public double calculation(string s) {

            string str = s;          
            
            foreach (char c in str)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    symbol = c;
                    string[] number = str.Split(new char[] { '+', '-', '*', '/' });
                    number1 = Convert.ToDouble(number[0]);
                    number2 = Convert.ToDouble(number[1]);
                    
                }
            }
            if (symbol == '+')
            {
                result = Sum(number1, number2);

            }
            if (symbol == '-')
            {
                result = Subtraction(number1, number2);

            }
            if (symbol == '*')
            {
                result = Multiplication(number1, number2);

            }
            if (symbol == '/')
            {
                result = Division(number1, number2);

            }
            return result;

        }
        public double Sum(double n1, double n2)
        {
            return n1 + n2;
        }
        public double Subtraction(double n1, double n2)
        {
            return n1 - n2;
        }
        public double Multiplication(double n1, double n2)
        {
            return n1 * n2;
        }
        public double Division(double n1, double n2)
        {
            if (n2 != 0)
            {
                return n1 / n2;
            }
            else throw new DivideByZeroException();
        }
    }
}
