namespace LoxLang
{
    public class Token
    {
        private readonly TokenType _type;
        private readonly string _lexeme;
        private readonly object _literal;
        private readonly int _line;

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            _type = type;
            _lexeme = lexeme;
            _literal = literal;
            _line = line;
        }

        public TokenType Type => _type;

        public string Lexeme => _lexeme;

        public object Literal => _literal;

        public int Line => _line;

        public override string ToString()
        {
            return Type + " " + Lexeme + " " + Literal;
        }

    }
}
