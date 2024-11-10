namespace LoxLang
{
    public class ParseError : Exception
    {
        public ParseError() : base() { }
        public ParseError(string message) : base(message) { }
        public ParseError(string message, Exception inner) : base(message, inner) { }
    }
}
