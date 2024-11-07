namespace LoxLang
{
    public class Interpreter : Expr.IVisitor<Object>
    {
        public object VisitAssignExpr(Expr.Assign expr)
        {
            throw new NotImplementedException();
        }

        public object VisitBinaryExpr(Expr.Binary expr)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public object VisitLiteralExpr(Expr.Literal expr)
        {
            throw new NotImplementedException();
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

        public object VisitUnaryExpr(Expr.Unary expr)
        {
            throw new NotImplementedException();
        }

        public object VisitVariableExpr(Expr.Variable expr)
        {
            throw new NotImplementedException();
        }
    }
}
