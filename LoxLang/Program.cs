using System.Text;

namespace LoxLang
{
    internal class Program
    {
        private static bool hadError = false;

        private static void Main(string[] args)
        {
            //string Path = @"D:\Code.txt";
            //RunFile(Path);

            Expr.Binary expression = new(new Expr.Unary(
                                                         new Token(TokenType.MINUS, "-", null, 1),
                                                         new Expr.Literal(123)
                                                     ),
                                                     new Token(TokenType.STAR, "*", null, 1),
                                                     new Expr.Grouping(
                                                          new Expr.Literal(45.67)
                                                     ));

            Console.WriteLine(new AstPrinter().print(expression));
        }

        private static void RunFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);

            run(Encoding.UTF8.GetString(bytes));

            if (hadError)
            {
                Environment.Exit(65);
            }
        }

        private static void run(string source)
        {
            Scanner scanner = new(source);
            List<Token> tokens = scanner.scanTokens();
            Parser parser  = new Parser(tokens);

            foreach (Token token in tokens)
            {
                Console.WriteLine(token);
            }

        }

        public static void error(int line, string message)
        {
            report(line, "", message);
        }
        private static void report(int line, string where, string message)
        {
            Console.WriteLine("[line " + line + "] Error" + where + ": " + message);
            hadError = true;
        }

        public static void error(Token token, String message)
        {
            if (token.Type == TokenType.EOF)
            {
                report(token.Line, " at end", message);
            }
            else
            {
                report(token.Line, " at '" + token.Lexeme + "'", message);
            }
        }


    }
}
