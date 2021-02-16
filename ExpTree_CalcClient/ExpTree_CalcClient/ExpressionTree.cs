using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpTree_CalcClient
{
  class ExpressionTree
  {
    public static Stack<Expression> expList = new Stack<Expression>();//Constant
    public static Stack<ConstantExpression> opExpList = new Stack<ConstantExpression>();//MakeTree
   
    public static Expression ParsingExpression(char[] exp)
    {
      opExpList.Push(Expression.Constant('('));
      int pos = 0;
      while (pos <= exp.Length)
      {
        if (pos == exp.Length || exp[pos] == ')')
        {
          while (Convert.ToChar(opExpList.Peek().Value)!= '(')
          {
            ExecuteOperation();
          }
          opExpList.Pop();
          pos++;
        }
        else if (exp[pos] >= '0' && exp[pos] <= '9')
        {
          pos = ProcessInputNumber(exp, pos);
        }
        else
        {
          ProcessInputOperator(exp[pos]);
          pos++;
        }
      }
      return expList.Pop();
    }
    private static int ProcessInputNumber(char[] exp, int pos)
    {

      int value = 0;
      while (pos < exp.Length && exp[pos] >= '0' && exp[pos] <= '9')
        value = 10 * value + (int)(exp[pos++] - '0');

      expList.Push(Expression.Constant(value));

      return pos;
    }
    private static void ProcessInputOperator(char op)
    {
      while (opExpList.Count > 0 && OperatorCausesEvaluation(op, Convert.ToChar(opExpList.Peek().Value)))
      {
        ExecuteOperation();
      }

      opExpList.Push(Expression.Constant(op));
    }
    private static bool OperatorCausesEvaluation(char op, char prevOp)
    {
      bool evaluate = false;
      switch (op)
      {
        case '+':
        case '-':
          evaluate = (prevOp != '(');
          break;
        case '*':
        case '/':
          evaluate = (prevOp == '*' || prevOp == '/');
          break;
        case ')':
          evaluate = true;
          break;
      }
      return evaluate;
    }
    //Создаётся дерево
    private static void ExecuteOperation()
    {
      Expression rightOperand;
      Expression leftOperand;
      try
      {
        rightOperand = expList.Pop();
        leftOperand = expList.Pop();
      }
      catch
      {
        throw new Exception("Строка имеет не верный формат");
      }
      ConstantExpression op = opExpList.Pop();
      BinaryExpression result;
      switch (op.Value)
      {
        case '+': result = Expression.MakeBinary(ExpressionType.Add, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Calculating).GetMethod("GetResponsiPlus")); break;
        case '-': result = Expression.MakeBinary(ExpressionType.Subtract, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Calculating).GetMethod("GetResponsiMin")); break;
        case '*': result = Expression.MakeBinary(ExpressionType.Multiply, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Calculating).GetMethod("GetResponsiMult")); break;
        case '/': result = Expression.MakeBinary(ExpressionType.Divide, Expression.Constant(leftOperand), Expression.Constant(rightOperand), false, typeof(Calculating).GetMethod("GetResponsiDel")); break;
        default: throw new ArgumentException();
      }
      expList.Push(result);
    }
  }
}
