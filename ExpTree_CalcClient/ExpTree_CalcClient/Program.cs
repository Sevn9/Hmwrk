using System;
using System.Linq.Expressions;

namespace ExpTree_CalcClient
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Введите выражение");
      string expression = Console.ReadLine();
      //убираем пробелы и преобразуем строку в массив символов
      var mas = expression.Replace(" ", "").ToCharArray();

      Expression Tree = ExpressionTree.ParsingExpression(mas);
      Console.WriteLine("Получившееся дерево");
      Console.WriteLine(Tree.ToString());
      //считаем
      var ans = Calculating.Calculate(Tree).Result;
      Console.WriteLine("Ответ: " + ans.ToString());
    }
  }
}
