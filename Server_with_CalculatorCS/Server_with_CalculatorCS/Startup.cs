using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_with_CalculatorCS
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseRouting();

      //если в запросе указан и a и oper и b то обрабатывается ф-ей Calculating
      app.MapWhen(context =>
      {
        return context.Request.Query.ContainsKey("a") && context.Request.Query.ContainsKey("oper") &&
                context.Request.Query.ContainsKey("b");
      }, Calculating);

        //app.UseMiddleware<CalcMiddleware>();
        //app.UseValue();

        
      app.UseEndpoints(endpoints =>
      {
        app.Run (async context =>
        {
            await context.Response.WriteAsync("Hello World!");
        });
      });
      static void Calculating(IApplicationBuilder app)
      {
        app.UseValue();
      }
    }
  }
}
