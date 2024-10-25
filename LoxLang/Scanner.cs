namespace LoxLang
{
    public class Scanner
    {
        private readonly string _source;
        private readonly List<Token> _tokens = new List<Token>();
        private static readonly Dictionary<string,TokenType> _keywords;

        static Scanner()
        {
            _keywords = new Dictionary<string,TokenType>();
            _keywords.Add("and",TokenType.AND);
            _keywords.Add("class", TokenType.CLASS);
            _keywords.Add("else", TokenType.ELSE);
            _keywords.Add("false", TokenType.FALSE);
            _keywords.Add("for", TokenType.FOR);
            _keywords.Add("fun", TokenType.FUN);
            _keywords.Add("if", TokenType.IF);
            _keywords.Add("nil", TokenType.NIL);
            _keywords.Add("or", TokenType.OR);
            _keywords.Add("print", TokenType.PRINT);
            _keywords.Add("return", TokenType.RETURN);
            _keywords.Add("super", TokenType.SUPER);
            _keywords.Add("this", TokenType.THIS);
            _keywords.Add("true", TokenType.TRUE);
            _keywords.Add("var", TokenType.VAR);
            _keywords.Add("while", TokenType.WHILE);
        }

        //The start field points to the first character in the lexeme being scanned
        private int _start = 0;

        // The current points at the character currently being considered
        private int _current = 0;

        //The line field tracks what source line current is on so we can produce tokens that know their location
        private int _line = 1;

        public Scanner(string source)
        {
            _source = source;
        }

        public List<Token> scanTokens()
        {
            while (!isAtEnd())
            {
                _start = _current;
                scanToken();
            }

            _tokens.Add(new Token(TokenType.EOF, "", null, _line));
            return _tokens;
        }

        private void scanToken()
        {
            char c = advance();
            switch (c)
            {
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '{': addToken(TokenType.LEFT_BRACE); break;
                case '}': addToken(TokenType.RIGHT_BRACE); break;
                case ',': addToken(TokenType.COMMA); break;
                case '.': addToken(TokenType.DOT); break;
                case '-': addToken(TokenType.MINUS); break;
                case '+': addToken(TokenType.PLUS); break;
                case ';': addToken(TokenType.SEMICOLON); break;
                case '*': addToken(TokenType.STAR); break;
                case '!':
                    addToken(match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                    break;
                case '=':
                    addToken(match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                    break;
                case '<':
                    addToken(match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;
                case '>':
                    addToken(match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;
                case '/':
                    if (match('/'))
                    {
                        while (peek() != '\n' && !isAtEnd())
                            advance();
                    }
                    else
                    {
                        addToken(TokenType.SLASH);
                    }
                    break;
                case ' ':
                case '\r':
                case '\t':
                    // Ignore whitespace.
                    break;
                case '\n':
                    _line++;
                    break;
                case '"':
                    stringToken();
                    break;
                default:
                    if (isDigit(c))
                        number();
                    else
                        Program.error(_line, "Unexpected character.");
                    break;
            }
        }

        private void number()
        {
            while (isDigit(peek())) advance();
            // Look for a fractional part.
            if (peek() == '.' && isDigit(peekNext()))
            {
                // Consume the "."
                advance();
                while (isDigit(peek())) advance();
            }
            addToken(TokenType.NUMBER,double.Parse(_source.Substring(_start, _current)));

        }

        private char peekNext()
        {
            if (_current + 1 >= _source.Length) return '\0';
            return _source[_current + 1];
        }

        private bool isDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private void stringToken()
        {
            while (peek() != '"' && !isAtEnd())
            {
                if (peek() == '\n') _line++;
                advance();
            }
            if (isAtEnd())
            {
                Program.error(_line, "Unterminated string.");
                return;
            }
            // The closing ".
            advance();

            // Trim the surrounding quotes.
            String value = _source.Substring(_start + 1, _current - 1);
            addToken(TokenType.STRING, value);
        }

        private char peek()
        {
            if(isAtEnd())
                return '\n';

            return _source[_current];
        }

        private bool match(char expected)
        {
            if(isAtEnd())
                return false;

            if (_source[_current] != expected)
                return false;

            _current++;

            return true;

        }

        private void addToken(TokenType type)
        {
            addToken(type, null);
        }

        private void addToken(TokenType type, object literal)
        {
            string text = _source.Substring(_start,_current);
            _tokens.Add(new Token(type,text,literal,_line));
        }

        private char advance()
        {
            _current++;
            return _source[_current - 1];
        }

        private bool isAtEnd()
        {
            return _current >= _source.Length;
        }
    }
}
