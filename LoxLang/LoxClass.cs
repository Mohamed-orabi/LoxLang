
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
            LoxFunction initializer = findMethod("init");
            if (initializer != null)
            {
                initializer.bind(instance).call(interpreter, arguments);
            }
            return instance;
        }

        public LoxFunction findMethod(string name)
        {
            if (_methods.ContainsKey(name))
            {
                return _methods[name];
            }

            return null;
        }

        public int arity()
        {
            LoxFunction initializer = findMethod("init");
            if (initializer == null) return 0;
            return initializer.arity();
        }
        public override string ToString()
        {
            return _name;
        }
    }
}
