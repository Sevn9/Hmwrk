using System;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

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
      //-------------------------
      IServiceCollection services = new ServiceCollection();
      services.AddSingleton<ICalculating, LCalculating>(); 
      var servicesBuild = services.BuildServiceProvider();
      var d = servicesBuild.GetService<ICalculating>();

      ICalculating r = d;
      Calculating expTree = new Calculating(r);
      //---------------
      Expression Tree = ExpressionTree.ParsingExpression(mas);
      Console.WriteLine("Получившееся дерево");
      Console.WriteLine(Tree.ToString());
      //------------------
      var ans1 = Expression.Lambda<Func<double>>(Tree).Compile()();
      Console.WriteLine(ans1);
      //считаем
      var ans = expTree.Calculate(Tree).Result;
      Console.WriteLine("Ответ: " + ans.ToString());
    }
  }
}
