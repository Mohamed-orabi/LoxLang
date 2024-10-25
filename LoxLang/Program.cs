using System.Text;

namespace LoxLang
{
    internal class Program
    {
        static bool hadError = false;

        static void Main(string[] args)
        {
            string Path = @"D:\Code.txt";
            RunFile(Path);
        }

        private static void RunFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            run(Encoding.Default.GetString(bytes));
            if (hadError) Environment.Exit(65);
        }

        private static void run(String source)
        {
            //Scanner scanner = new Scanner(source);
            //List<Token> tokens = scanner.scanTokens();
            //// For now, just print the tokens.
            //for (Token token : tokens)
            //{
            //    System.out.println(token);
            //}
        }

        static void error(int line, String message)
        {
            report(line, "", message);
        }
        private static void report(int line, String where,
        String message)
        {
            Console.WriteLine(
            "[line " + line + "] Error" + where + ": " + message);
            hadError = true;
        }
    }
}
