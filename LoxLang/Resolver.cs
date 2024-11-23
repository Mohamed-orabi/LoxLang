using static LoxLang.Expr;
using static LoxLang.Stmt;
using static LoxLang.TokenType;

namespace LoxLang
{
    public class Resolver : Expr.IVisitor<object>, Stmt.IVisitor<object>
    {
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
            throw new NotImplementedException();
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
