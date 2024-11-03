
namespace LoxLang
{
    public class Parser
    {
        public List<Token> _tokens;
        private int current = 0;
        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        private Expr expression()
        {
            return equality();
        }

        private Expr equality()
        {
            Expr expr = comparison();

            while (match(TokenType.BANG_EQUAL, TokenType.EQUAL_EQUAL))
            {
                Token op = previous();
                Expr right = comparison();
                expr = new Expr.Binary(expr, op, right);
            }

            return expr;
        }

        private Expr comparison()
        {
            Expr expr = term();

            while (match(TokenType.GREATER, TokenType.GREATER_EQUAL, TokenType.LESS, TokenType.LESS_EQUAL))
            {
                Token op = previous();
                Expr right = term();
                expr = new Expr.Binary(expr, op, right);
            }

            return expr;
        }

        private Expr term()
        {
            Expr expr = factor();

            while (match(TokenType.MINUS, TokenType.PLUS))
            {
                Token op = previous();
                Expr right = factor();
                expr = new Expr.Binary(expr, op, right);
            }

            return expr;
        }

        private Expr factor()
        {
            Expr expr = unary();

            while (match(TokenType.SLASH, TokenType.STAR))
            {
                Token op = previous();
                Expr right = unary();
                expr = new Expr.Binary(expr, op, right);
            }

            return expr;
        }

        private Expr unary()
        {
            if (match(TokenType.BANG, TokenType.MINUS))
            {
                Token op = previous();
                Expr right = unary();
                return new Expr.Unary(op, right);
            }

            return primary();
        }

        private Expr primary()
        {
            if (match(TokenType.FALSE))
            {
                return new Expr.Literal(false);
            }

            if (match(TokenType.TRUE))
            {
                return new Expr.Literal(true);
            }

            if (match(TokenType.NIL))
            {
                return new Expr.Literal(null);
            }

            if (match(TokenType.NUMBER, TokenType.STRING))
            {
                return new Expr.Literal(previous().Literal);
            }

            if (match(TokenType.LEFT_PAREN))
            {
                Expr expr = expression();
                //consume(TokenType.RIGHT_PAREN, "Expect ')' after expression.");
                return new Expr.Grouping(expr);
            }

            return null;
        }

        private Token previous()
        {
            return _tokens[current - 1];
        }

        private bool match(params TokenType[] tokens)
        {
            foreach (TokenType item in tokens)
            {
                if (check(item))
                {
                    _ = advance();
                    return true;
                }
            }

            return false;
        }

        private Token advance()
        {
            if (!isAtEnd())
            {
                current++;
            }

            return previous();
        }


        private bool isAtEnd()
        {
            return peek().Type == TokenType.EOF;
        }

        private Token peek()
        {
            return _tokens[current];
        }

        private bool check(TokenType item)
        {
            return !isAtEnd() && peek().Type == item;
        }





    }
}
