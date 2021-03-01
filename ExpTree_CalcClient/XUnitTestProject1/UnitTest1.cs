using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Linq.Expressions;
using ExpTree_CalcClient;


namespace XUnitTestProject1
{
  public class UnitTest1
  {
    [Fact]
    public void Test1()
    {
      IServiceCollection services = new ServiceCollection();
      services.AddSingleton<ICalculating, LCalculating>();
      var servicesBuild = services.BuildServiceProvider();
      var d = servicesBuild.GetService<ICalculating>();
      var mas = "15*5".Replace(" ", "").ToCharArray();
      Calculating expTree = new Calculating(d);
      Expression Tree = ExpressionTree.ParsingExpression(mas);
      Assert.Throws<AggregateException>(() => (expTree.Calculate(Tree).Result, Expression.Lambda<Func<double>>(Tree).Compile()()));
    }
  }
}
