
using System.Text;

namespace LoxLang
{
    public class Environment
    {
        private Environment _enclosing;
        private static Dictionary<string, object> values = new Dictionary<string, object>();

        public Environment(Environment enclosing)
        {
            _enclosing = enclosing;
        }

        public Environment()
        {
                
        }

        public void define(string name, object value)
        {
            values[name] = value;
        }

        public object get(Token name)
        {
            if (values.ContainsKey(name.Lexeme))
                return values[name.Lexeme];

            if (_enclosing != null) return _enclosing.get(name);

            throw new RuntimeError(name, "Undefined variable '" + name.Lexeme + "'.");
        }

        public void assign(Token name, Object value)
        {
            if (values.ContainsKey(name.Lexeme))
            {
                values[name.Lexeme] = value;
            }

            if (_enclosing != null)
            {
                _enclosing.assign(name, value);
            }

            throw new RuntimeError(name, "Undefined variable '" + name.Lexeme + "'.");
        }
    }
}

