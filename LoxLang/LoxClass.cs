
namespace LoxLang
{
    public class LoxClass : LoxCallable
    {
        public readonly string _name;

        public LoxClass(string name)
        {
            _name = name;
        }

        public object call(Interpreter interpreter, List<object> arguments)
        {
            LoxInstance instance = new LoxInstance(this);
            return instance;
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
