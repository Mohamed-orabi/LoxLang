using System;
using System.Text;

namespace LoxLang
{
    internal class Program
    {
        private static bool hadError = false;
        static bool hadRuntimeError = false;
        private static void Main(string[] args)
        {
            string Path = @"D:\Code.txt";
            RunFile(Path);
        }

        private static void RunFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);

            run(Encoding.UTF8.GetString(bytes));

            if (hadError)
                System.Environment.Exit(65);

            if (hadRuntimeError)
                System.Environment.Exit(65);
        }

        private static void run(string source)
        {
            Scanner scanner = new(source);
            List<Token> tokens = scanner.scanTokens();
            Parser parser = new Parser(tokens);
            var expr = parser.parse();
            Interpreter interpreter = new Interpreter();
            interpreter.interpret(expr);
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

        public static void runtimeError(RuntimeError error)
        {
            Console.WriteLine(error.Message + "\n[line " + error.Token.Line + "]");
            hadRuntimeError = true;
        }


    }
}
