
namespace LoxLang
{
    public class LoxClass : LoxCallable
    {
        public readonly string _name;
        public readonly Dictionary<string, LoxFunction> _methods;

        public LoxClass(string name, Dictionary<string, LoxFunction> methods)
        {
            _name = name;
            _methods = methods;
        }

        public object call(Interpreter interpreter, List<object> arguments)
        {
            LoxInstance instance = new LoxInstance(this);
            return instance;
        }

        public LoxFunction findMethod(String name)
        {
            if (_methods.ContainsKey(name))
            {
                return _methods[name];
            }

            return null;
        }

        public int arity()
        {
            return 0;
        }
        public override string ToString()
        {
            return _name;
        }
    }
}
