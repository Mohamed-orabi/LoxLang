using static LoxLang.Expr;
using static LoxLang.TokenType;

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


        public List<Stmt> parse()
        {
            List<Stmt> result = new List<Stmt>();
            while(!isAtEnd())
                result.Add(declaration()); 

            return result;
        }

        private Stmt declaration()
        {
            try
            {
                if (match(VAR)) return varDeclaration();

                return statement();
            }
            catch (ParseError error)
            {
                synchronize();
                return null;
            }
        }

        private Stmt varDeclaration()
        {
            Token name = consume(IDENTIFIER, "Expect variable name.");

            Expr initializer = null;
            if (match(EQUAL))
            {
                initializer = expression();
            }

            consume(SEMICOLON, "Expect ';' after variable declaration.");
            return new Stmt.Var(name, initializer);
        }

        private Stmt statement()
        {
            if (match(PRINT)) 
                return printStatement();

            if (match(LEFT_BRACE)) return new Stmt.Block(block());

            return expressionStatement();
        }

        private List<Stmt> block()
        {
            List<Stmt> statements = new List<Stmt> ();

            while (!check(RIGHT_BRACE) && !isAtEnd())
            {
                statements.Add(declaration());
            }

            consume(RIGHT_BRACE, "Expect '}' after block.");
            return statements;
        }

        private Stmt printStatement()
        {
            Expr expr = expression();
            consume(SEMICOLON, "Expect ';' after value.");
            return new Stmt.Print(expr);
        }

        private Stmt expressionStatement()
        {
            Expr expr = expression();
            consume(SEMICOLON, "Expect ';' after expression.");
            return new Stmt.Expression(expr);
        }

        public Expr Oldparse()
        {
            try
            {
                return expression();
            }
            catch (ParseError)
            {
                return null;
            }
        }

        private Expr expression()
        {
            return assignment();
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

        private Expr assignment()
        {
            Expr expr = equality();

            if (match(EQUAL))
            {
                Token equals = previous();
                Expr value = assignment();

                if (expr is Expr.Variable) {
                    Token name = ((Expr.Variable)expr).name;
                    return new Expr.Assign(name, value);
                }

                error(equals, "Invalid assignment target.");
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

            if (match(IDENTIFIER))
            {
                return new Expr.Variable(previous());
            }

            if (match(TokenType.LEFT_PAREN))
            {
                Expr expr = expression();
                _ = consume(RIGHT_PAREN, "Expect ')' after expression.");
                return new Expr.Grouping(expr);
            }

            throw error(peek(), "Expect expression.");
        }

        private Token consume(TokenType token, string message)
        {
            return check(token) ? advance() : throw error(peek(), message);
        }

        private Token previous()
        {
            return _tokens[current - 1];
        }


        private ParseError error(Token token, string message)
        {
            Program.error(token, message);
            return new ParseError();
        }

        private void synchronize()
        {
            _ = advance();

            while (!isAtEnd())
            {
                if (previous().Type == SEMICOLON)
                {
                    return;
                }

                switch (peek().Type)
                {
                    case CLASS:
                    case FUN:
                    case VAR:
                    case FOR:
                    case IF:
                    case WHILE:
                    case PRINT:
                    case RETURN:
                        return;
                }

                _ = advance();
            }
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
            if (isAtEnd()) 
                return false;

            return  peek().Type == item;
        }





    }
}
