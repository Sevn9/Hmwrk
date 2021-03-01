﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace ExpTree_CalcClient
{
  class ExpVisitor : ExpressionVisitor
  {
    public double doubleAns;
    public ConcurrentDictionary<string, Task<double>> tasks = new ConcurrentDictionary<string, Task<double>>();
    private ICalculating resp;
    public ExpVisitor(ICalculating resp)
    {
      this.resp = resp;
    }
    public override Expression Visit(Expression node)
    {
      tasks[node.ToString()] = Task.Run(async () =>
      {
        double a1 = 0;
        double.TryParse(node.ToString(), out a1);
        if (a1 != 0 || (node.ToString() == "0"))
          return a1;
        Expression a = ((BinaryExpression)node).Left;
        Expression b = ((BinaryExpression)node).Right;
        Visit(a);
        Visit(b);
        var ans = await Task.WhenAll(tasks[a.ToString()], tasks[b.ToString()]);
        double[] args = new double[] { ans[0], ans[1] };
        double globAns = 0;
        switch (node.NodeType)
        {
          case ExpressionType.Add:
            globAns = resp.GetPesponsing(args[0], args[1], "+");
            break;
          case ExpressionType.Multiply:
            globAns = resp.GetPesponsing(args[0], args[1], "*");
            break;
          case ExpressionType.Divide:
            globAns = resp.GetPesponsing(args[0], args[1], "/");
            break;
          case ExpressionType.Subtract:
            globAns = resp.GetPesponsing(args[0], args[1], "-");
            break;
          default: throw new ArgumentException();
        }
        doubleAns = globAns;
        return globAns;
      });
      return node;
    }
    public Task<double> Run(Expression Tree)
    {
      return this.tasks[Tree.ToString()];
    }
  }
}
