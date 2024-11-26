namespace LoxLang
{
    public class Environment
    {
        public Environment _enclosing;
        public Dictionary<string, object> values = new Dictionary<string, object>();

        public Environment(Environment enclosing)
        {
            _enclosing = enclosing;
        }

        public Environment()
        {
            _enclosing = null;
        }

        public void define(string name, object value)
        {
            values[name] = value;
        }

        public object get(Token name)
        {
            if (values.ContainsKey(name.Lexeme))
                return values[name.Lexeme];

            if (_enclosing != null) 
                return _enclosing.get(name);

            throw new RuntimeError(name, "Undefined variable '" + name.Lexeme + "'.");
        }

        public void assign(Token name, object value)
        {
            if (values.ContainsKey(name.Lexeme))
            {
                values[name.Lexeme] = value;
                return;
            }


            if (_enclosing != null)
            {
                _enclosing.assign(name, value);
                return;
            }

            throw new RuntimeError(name, "Undefined variable '" + name.Lexeme + "'.");
        }

        public object getAt(int distance, String name)
        {
            return ancestor(distance).values[name];
        }


        public void assignAt(int distance, Token name, Object value)
        {
            ancestor(distance).values[name.Lexeme] =  value;
        }
        Environment ancestor(int distance)
        {
            Environment environment = this;
            for (int i = 0; i < distance; i++)
            {
                environment = environment._enclosing;
            }

            return environment;
        }
    }
}

