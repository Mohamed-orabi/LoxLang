using static LoxLang.Stmt;

namespace LoxLang
{
    public class LoxInstance
    {
        private readonly LoxClass _klass;
        private readonly Dictionary<string,object> fields = new Dictionary<string, object>();

        public LoxInstance(LoxClass klass)
        {
            _klass = klass;
        }

        public object get(Token name)
        {
            if (fields.ContainsKey(name.Lexeme))
            {
                return fields[name.Lexeme];
            }

            LoxFunction method = _klass.findMethod(name.Lexeme);
            if (method != null) return method;

            throw new RuntimeError(name,
                "Undefined property '" + name.Lexeme + "'.");
        }

        public void set(Token name, object value)
        {
            fields[name.Lexeme] =  value;
        }

        public override string ToString()
        {
            return _klass._name + " instance";
        }
    }
}
