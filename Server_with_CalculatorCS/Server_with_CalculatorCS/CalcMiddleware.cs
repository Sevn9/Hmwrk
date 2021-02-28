using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Server_with_CalculatorCS
{
  public class CalcMiddleware
  {
    //через данный объект сможем обращаться к следующему компоненту в конвейере и передавать ему управление обработкой запросов
    private readonly RequestDelegate _next;

    //конструктор 
    public CalcMiddleware(RequestDelegate next) 
    {
      _next = next;
    }
    //когда компонент получет запрос на обработку, этот метод обрабатывает запрос
    public async Task InvokeAsync(HttpContext context)
    {
      //логика обработки запросов

      // var value = context.Request.Query["value"];
      string ans = "";
      if (context.Request.Query["oper"] == "/" ||
          context.Request.Query["oper"] == "+" ||
          context.Request.Query["oper"] == "-" ||
          context.Request.Query["oper"] == "*")
      {
        ans = (new Calculator.ClassCalculations().Calculation(double.Parse(context.Request.Query["a"]), 
          char.Parse(context.Request.Query["oper"]), double.Parse(context.Request.Query["b"]))).ToString();

      }
      else
      {
        context.Response.StatusCode = 403;
        ans = "value is not found";       
      }
      await context.Response.WriteAsync(ans);
      //вызов следующего компонента в конвейере
      await _next.Invoke(context);
    }

  }
  public static class ValueExtensions
  {
    public static IApplicationBuilder UseValue(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<CalcMiddleware>();
    }
  }
}
