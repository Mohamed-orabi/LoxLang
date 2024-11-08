using System;
using static LoxLang.TokenType;

namespace LoxLang
{
    public class Interpreter : Expr.IVisitor<object>
    {

        public void interpret(Expr expression)
        {
            try
            {
                Object value = evaluate(expression);
                Console.WriteLine(value.ToString());
                //System.out.println(stringify(value));
            }
            catch (RuntimeError error)
            {
                Program.runtimeError(error);
            }
        }

        public object VisitAssignExpr(Expr.Assign expr)
        {
            throw new NotImplementedException();
        }

        public object VisitBinaryExpr(Expr.Binary expr)
        {
            var right = evaluate(expr.right);
            var left = evaluate(expr.left);

            switch (expr.op.Type)
            {
                case MINUS:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left - (double)right;
                case SLASH:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left / (double)right;
                case STAR:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left * (double)right;
                case PLUS:
                    if (left is double && right is double)
                    {
                        return (double)left + (double)right;
                    }

                    if (left is string && right is string)
                    {
                        return (string)left + (string)right;
                    }
                    checkNumberOperand(expr.op, "Operands must be two numbers or two strings.");
                    break;
                case GREATER:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left > (double)right;
                case GREATER_EQUAL:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left >= (double)right;
                case LESS:
                    checkNumberOperands(expr.op, left, right);
                    return (double)left < (double)right;
                case LESS_EQUAL:
                    return (double)left <= (double)right;
                case BANG_EQUAL: return !isEqual(left, right);
                case EQUAL_EQUAL: return isEqual(left, right);
            }

            return null;
        }

        private bool isEqual(Object a, Object b)
        {
            if (a == null && b == null) return true;
            if (a == null) return false;

            return a.Equals(b);
        }

        public object VisitCallExpr(Expr.Call expr)
        {
            throw new NotImplementedException();
        }

        public object VisitGetExpr(Expr.Get expr)
        {
            throw new NotImplementedException();
        }

        public object VisitGroupingExpr(Expr.Grouping expr)
        {
            return evaluate(expr.expression);
        }

        private object evaluate(Expr expression)
        {
            return expression.Accept(this);
        }

        public object VisitLiteralExpr(Expr.Literal expr)
        {
            return expr.value;
        }

        public object VisitLogicalExpr(Expr.Logical expr)
        {
            throw new NotImplementedException();
        }

        public object VisitSetExpr(Expr.Set expr)
        {
            throw new NotImplementedException();
        }

        public object VisitSuperExpr(Expr.Super expr)
        {
            throw new NotImplementedException();
        }

        public object VisitThisExpr(Expr.This expr)
        {
            throw new NotImplementedException();
        }

        // Operators: -, +, and *
        // Operands: 5, 3, and 2
        public object VisitUnaryExpr(Expr.Unary expr)
        {
            object right = evaluate(expr.right);

            switch (expr.op.Type)
            {
                case MINUS:
                    checkNumberOperand(expr.op, right);
                    return -(double)right;
                case BANG:
                    return !isTruthy(right);
            }

            return null;
        }

        private void checkNumberOperand(Token token, object operand)
        {
            if (operand is double)
                return;

            throw new RuntimeError(token, "Operand must be a number.");
        }

        private void checkNumberOperands(Token op,
                                   object left, object right)
        {
            if (left is double && right is double) return;

            throw new RuntimeError(op, "Operands must be numbers.");
        }

        private bool isTruthy(object obj)
        {
            if (obj == null)
                return false;

            if (obj is bool)
                return (bool)obj;

            return true;
        }

        public object VisitVariableExpr(Expr.Variable expr)
        {
            throw new NotImplementedException();
        }
    }
}
