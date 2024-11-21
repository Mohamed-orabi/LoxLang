using static LoxLang.TokenType;
namespace LoxLang
{
    public class Scanner
    {
        private readonly string _source;
        private readonly List<Token> _tokens = new List<Token>();
        private static readonly Dictionary<string, TokenType> _keywords;

        static Scanner()
        {
            _keywords = new Dictionary<string, TokenType>
            {
                { "and", AND },
                { "class", CLASS },
                { "else", ELSE },
                { "false", FALSE },
                { "for", FOR },
                { "fun", FUN },
                { "if", IF },
                { "nil", NIL },
                { "or", OR },
                { "print", PRINT },
                { "return", RETURN },
                { "super", SUPER },
                { "this", THIS },
                { "true", TRUE },
                { "var", VAR },
                { "while",WHILE }
            };
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

            _tokens.Add(new Token(EOF, "", null, _line));
            return _tokens;
        }

        private void scanToken()
        {
            char c = advance();
            switch (c)
            {
                case '(': addToken(LEFT_PAREN); break;
                case ')': addToken(RIGHT_PAREN); break;
                case '{': addToken(LEFT_BRACE); break;
                case '}': addToken(RIGHT_BRACE); break;
                case ',': addToken(COMMA); break;
                case '.': addToken(DOT); break;
                case '-': addToken(MINUS); break;
                case '+': addToken(PLUS); break;
                case ';': addToken(SEMICOLON); break;
                case '*': addToken(STAR); break;
                case '!':
                    addToken(match('=') ? BANG_EQUAL : BANG);
                    break;
                case '=':
                    addToken(match('=') ? EQUAL_EQUAL : EQUAL);
                    break;
                case '<':
                    addToken(match('=') ? LESS_EQUAL : LESS);
                    break;
                case '>':
                    addToken(match('=') ? GREATER_EQUAL : GREATER);
                    break;
                case '/':
                    if (match('/'))
                    {
                        while (peek() != '\n' && !isAtEnd())
                            advance();
                    }
                    else
                    {
                        addToken(SLASH);
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
                case 'o':
                    if (match('r'))
                    {
                        addToken(OR);
                    }
                    break;
                default:
                    if (isDigit(c))
                        number();
                    else if (isAlpha(c))
                        identifier();
                    else
                        Program.error(_line, "Unexpected character.");
                    break;
            }
        }

        private bool isAlphaNumeric(char c)
        {
            return isAlpha(c) || isDigit(c);
        }
        private void identifier()
        {
            TokenType type;

            while (isAlphaNumeric(peek())) 
                advance();

            string text = _source.Substring(_start,(_current-_start));

            if(!_keywords.ContainsKey(text))
                addToken(IDENTIFIER);
            else
            {
                type = _keywords[text];
                addToken(type);
            }

            
        }

        private bool isAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
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
            addToken(NUMBER, double.Parse(_source.Substring(_start, (_current - _start))));

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
                if (peek() == '\n') 
                    _line++;

                advance();
            }
            if (isAtEnd())
            {
                Program.error(_line, "Unterminated string.");
                return;
            }
            // The closing ".
            advance();

            int start = _start + 1;
            int end = _current - 1;
            // Trim the surrounding quotes.
            string value = _source.Substring(_start + 1, (end - start));
            addToken(STRING, value);
        }

        private char peek()
        {
            if (isAtEnd())
                return '\n';

            return _source[_current];
        }

        private bool match(char expected)
        {
            if (isAtEnd())
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
            string text = _source.Substring(_start, (_current - _start));
            _tokens.Add(new Token(type, text, literal, _line));
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
