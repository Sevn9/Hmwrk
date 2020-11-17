using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
using System;

namespace Calculator.Test
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
		public void Sum_2Plus5_7Returned()
		{
			// arrange
			var calc = new Calculator();
			double a = 2;
			double b = 5;

			// act
			var res = calc.Sum(a, b);

			// assert
			Assert.AreEqual(7, res);

		}
		[TestMethod]
		public void Subtraction_5Minus2_3Returned()
		{
			// arrange
			var calc = new Calculator();
			double a = 5;
			double b = 2;

			// act
			var res = calc.Subtraction(a, b);

			// assert
			Assert.AreEqual(3, res);

		}
		[TestMethod]
		public void Multiplication_5Multiply2_10Returned()
		{
			// arrange
			var calc = new Calculator();
			double a = 5;
			double b = 2;

			// act
			var res = calc.Multiplication(a, b);

			// assert
			Assert.AreEqual(10, res);

		}
		[TestMethod]
		public void Division_15Devide3_5Returned()
		{
			// arrange
			var calc = new Calculator();
			double a = 15;
			double b = 3;

			// act
			var res = calc.Division(a,b);

			// assert
			Assert.AreEqual(5, res);

		}
		[TestMethod]
		public void Div_5div0_ZeroDivisionExceptionReturned()
		{
			var calc = new Calculator();
			string s = "5/0";

			// act
			void TmpMethod()
			{
				var res = calc.Calculation(s);
			}
			// assert
			Assert.ThrowsException<DivideByZeroException>(TmpMethod);
		}
		[TestMethod]
		public void Calculation_5Plus3_8Returned()
		{
			// arrange
			var calc = new Calculator();
			string s = "2+5";

			// act
			var res = calc.Calculation(s);

			// assert
			Assert.AreEqual(7, res);

		}
	}
}
