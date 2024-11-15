using System;
using static LoxLang.TokenType;

namespace LoxLang
{
    public class Interpreter : Expr.IVisitor<object>,
                               Stmt.IVisitor<object>
    {
        private Environment _environment = new Environment();

        public void interpret(List<Stmt> statements)
        {
            try
            {
                foreach (Stmt stmt in statements)
                {
                    executeStmt(stmt);
                }
            }
            catch (RuntimeError error)
            {
                Program.runtimeError(error);
            }
        }

        private void executeStmt(Stmt stmt)
        {
            stmt.Accept(this);
        }

        public object VisitBinaryExpr(Expr.Binary expr)
        {
            var right = executeExpr(expr.right);
            var left = executeExpr(expr.left);

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

        private bool isEqual(object a, object b)
        {
            if (a == null && b == null) return true;
            if (a == null) return false;

            return a.Equals(b);
        }



        public object VisitGroupingExpr(Expr.Grouping expr)
        {
            return executeExpr(expr.expression);
        }

        private object executeExpr(Expr expression)
        {
            return expression.Accept(this);
        }

        public object VisitLiteralExpr(Expr.Literal expr)
        {
            return expr.value;
        }



        // Operators: -, +, and *
        // Operands: 5, 3, and 2
        public object VisitUnaryExpr(Expr.Unary expr)
        {
            object right = executeExpr(expr.right);

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

        public object VisitExpressionStmt(Stmt.Expression stmt)
        {
            executeExpr(stmt.expression);
            return null;
        }



        public object VisitPrintStmt(Stmt.Print stmt)
        {
            object value = executeExpr(stmt.expression);
            Console.WriteLine(value);
            return null;
        }

        public object VisitVarStmt(Stmt.Var stmt)
        {
            object value = null;
            if (stmt.initializer != null)
            {
                value = executeExpr(stmt.initializer);
            }

            _environment.define(stmt.name.Lexeme, value);
            return null;
        }

        public object VisitVariableExpr(Expr.Variable expr)
        {
            return _environment.get(expr.name);
        }

        public object VisitAssignExpr(Expr.Assign expr)
        {
            object value = executeExpr(expr.value);
            _environment.assign(expr.name, value);
            return value;
        }

        public object VisitBlockStmt(Stmt.Block stmt)
        {
            executeBlock(stmt.statements, new Environment(_environment));
            return null;
        }

        void executeBlock(List<Stmt> statements,
                    Environment environment)
        {
            Environment previous = _environment;
            try
            {
                _environment = environment;

                foreach (Stmt stmt in statements)
                {
                    executeStmt(stmt);
                }

            }
            finally
            {
                _environment = previous;
            }
        }
        public object VisitIfStmt(Stmt.If stmt)
        {
            if (isTruthy(executeExpr(stmt.condition)))
                executeStmt(stmt.thenBranch);
            else if (stmt.elseBranch != null)
                executeStmt(stmt.elseBranch);

            return null;

        }

        public object VisitLogicalExpr(Expr.Logical expr)
        {
            object left = executeExpr(expr.left);

            if (expr.op.Type == OR)
            {
                if (isTruthy(left))
                    return left;
            }
            else
            {
                if (!isTruthy(left))
                    return left;
            }

            return executeExpr(expr.right);
        }

        public object VisitWhileStmt(Stmt.While stmt)
        {
            while (isTruthy(executeExpr(stmt.condition)))
                executeStmt(stmt.body);

            return null;
        }

        #region NotUsedYet


        public object VisitCallExpr(Expr.Call expr)
        {
            throw new NotImplementedException();
        }
        public object VisitGetExpr(Expr.Get expr)
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
        public object VisitClassStmt(Stmt.Class stmt)
        {
            throw new NotImplementedException();
        }
        public object VisitFunctionStmt(Stmt.Function stmt)
        {
            throw new NotImplementedException();
        }
        public object VisitReturnStmt(Stmt.Return stmt)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
