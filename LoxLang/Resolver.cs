using static LoxLang.Expr;
using static LoxLang.Stmt;
using static LoxLang.TokenType;

namespace LoxLang
{
    public class Resolver : Expr.IVisitor<object>, 
                            Stmt.IVisitor<object>
    {
        private readonly Interpreter _interpreter;
        private readonly Stack<Dictionary<string,bool>> scopes = new Stack<Dictionary<string,bool>>();
        private FunctionType currentFunction = FunctionType.NONE;

        public Resolver(Interpreter interpreter)
        {
            _interpreter = interpreter;
        }

        public void resolve(List<Stmt> statements)
        {
            foreach (Stmt item in statements)
            {
                resolve(item);
            }
        }

        private void beginScope()
        {
            scopes.Push(new Dictionary<string, bool>());
        }

        private void endScope()
        {
            scopes.Pop();
        }
        private void declare(Token name)
        {
            if (scopes.Count == 0) 
                return;

            var scope = scopes.Peek();

            if (scope.ContainsKey(name.Lexeme))
            {
                Program.error(name,
                    "Already a variable with this name in this scope.");
            }

            scope[name.Lexeme] =  false;
        }

        private void define(Token name)
        {
            if (scopes.Count == 0)
                return;

            var scope = scopes.Peek();
            scope[name.Lexeme] = true;
        }


        private void resolve(Stmt stmt)
        {
            stmt.Accept(this);
        }

        private void resolve(Expr expr)
        {
            expr.Accept(this);
        }

        public object VisitAssignExpr(Assign expr)
        {
            resolve(expr.value);
            resolveLocal(expr, expr.name);
            return null;
        }

        public object VisitBinaryExpr(Binary expr)
        {
            resolve(expr.left);
            resolve(expr.right);
            return null;
        }

        public object VisitBlockStmt(Block stmt)
        {
            beginScope();
            resolve(stmt.statements);
            endScope();
            return null;
        }

        public object VisitCallExpr(Call expr)
        {
            resolve(expr.callee);
            foreach (var item in expr.arguments)
            {
                resolve(item);
            }

            return null;
        }

        public object VisitClassStmt(Class stmt)
        {
            throw new NotImplementedException();
        }

        public object VisitExpressionStmt(Expression stmt)
        {
            resolve(stmt.expression);
            return null;
        }

        public object VisitFunctionStmt(Function stmt)
        {
            declare(stmt.name);
            define(stmt.name);

            resolveFunction(stmt, FunctionType.FUNCTION);
            return null;
        }

        private void resolveFunction(Stmt.Function function,FunctionType type)
        {
            FunctionType enclosingFunction = currentFunction;
            currentFunction = type;

            beginScope();

            foreach (Token item in function.param)
            {
                declare(item);
                define(item);
            }

            resolve(function.body);
            endScope();
            currentFunction = enclosingFunction;
        }

        public object VisitGetExpr(Get expr)
        {
            throw new NotImplementedException();
        }

        public object VisitGroupingExpr(Grouping expr)
        {
            resolve(expr.expression);
            return null;
        }

        public object VisitIfStmt(If stmt)
        {
            resolve(stmt.condition);
            resolve(stmt.thenBranch);
            if (stmt.elseBranch != null) 
                resolve(stmt.elseBranch);
            return null;
        }

        public object VisitLiteralExpr(Literal expr)
        {
            return null;
        }

        public object VisitLogicalExpr(Logical expr)
        {
            resolve(expr.left);
            resolve(expr.right);
            return null;
        }

        public object VisitPrintStmt(Print stmt)
        {
            resolve(stmt.expression);
            return null;
        }

        public object VisitReturnStmt(Stmt.Return stmt)
        {

            if (currentFunction == FunctionType.NONE)
            {
                Program.error(stmt.keyword, "Can't return from top-level code.");
            }

            if (stmt.value != null)
            {
                resolve(stmt.value);
            }

            return null;
        }

        public object VisitSetExpr(Set expr)
        {
            throw new NotImplementedException();
        }

        public object VisitSuperExpr(Super expr)
        {
            throw new NotImplementedException();
        }

        public object VisitThisExpr(This expr)
        {
            throw new NotImplementedException();
        }

        public object VisitUnaryExpr(Unary expr)
        {
            resolve(expr.right);
            return null;
        }

        public object VisitVariableExpr(Variable expr)
        {
            if (!(scopes.Count == 0) && scopes.Peek()[expr.name.Lexeme] == false)
            {
                Program.error(expr.name,
                    "Can't read local variable in its own initializer.");
            }

            resolveLocal(expr, expr.name);
            return null;
        }

        private void resolveLocal(Expr expr, Token name)
        {
            for (int i = scopes.Count - 1; i >= 0; i--)
            {
                if (scopes.ElementAt(i).ContainsKey(name.Lexeme))
                {
                    _interpreter.resolve(expr, scopes.Count - 1 - i);
                    return;
                }
            }
        }
        public object VisitVarStmt(Var stmt)
        {
            declare(stmt.name);
            if (stmt.initializer != null)
            {
                resolve(stmt.initializer);
            }
            define(stmt.name);
            return null;
        }

        public object VisitWhileStmt(While stmt)
        {
            resolve(stmt.condition);
            resolve(stmt.body);
            return null;
        }
    }
}
