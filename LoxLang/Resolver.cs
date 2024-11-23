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

        public Resolver(Interpreter interpreter)
        {
            _interpreter = interpreter;
        }

        void resolve(List<Stmt> statements)
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
            throw new NotImplementedException();
        }

        public object VisitBinaryExpr(Binary expr)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public object VisitClassStmt(Class stmt)
        {
            throw new NotImplementedException();
        }

        public object VisitExpressionStmt(Expression stmt)
        {
            throw new NotImplementedException();
        }

        public object VisitFunctionStmt(Function stmt)
        {
            throw new NotImplementedException();
        }

        public object VisitGetExpr(Get expr)
        {
            throw new NotImplementedException();
        }

        public object VisitGroupingExpr(Grouping expr)
        {
            throw new NotImplementedException();
        }

        public object VisitIfStmt(If stmt)
        {
            throw new NotImplementedException();
        }

        public object VisitLiteralExpr(Literal expr)
        {
            throw new NotImplementedException();
        }

        public object VisitLogicalExpr(Logical expr)
        {
            throw new NotImplementedException();
        }

        public object VisitPrintStmt(Print stmt)
        {
            throw new NotImplementedException();
        }

        public object VisitReturnStmt(Stmt.Return stmt)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public object VisitVariableExpr(Variable expr)
        {
            throw new NotImplementedException();
        }

        public object VisitVarStmt(Var stmt)
        {
            throw new NotImplementedException();
        }

        public object VisitWhileStmt(While stmt)
        {
            throw new NotImplementedException();
        }
    }
}
