
namespace LoxLang
{
    public class Environment
    {
        private static Dictionary<string, object> values = new Dictionary<string, object>();

        public void define(string name, object value)
        {
            values[name] = value;
        }

        public object get(Token name)
        {
            if (!values.ContainsKey(name.Lexeme))
                return values[name.Lexeme];

            throw new RuntimeError(name,"Undefined variable '" + name.Lexeme + "'.");
        }
    }
}

