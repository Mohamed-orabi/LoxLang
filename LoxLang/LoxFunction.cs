
using System.Xml.Linq;

namespace LoxLang
{
    public class LoxFunction : LoxCallable
    {
        private readonly Stmt.Function _declaration;
        private readonly Environment _closure;
        private readonly bool _isInitializer;

        public LoxFunction(Stmt.Function declaration,Environment closure,bool isInitializer)
        {
            _declaration = declaration;
            _closure = closure;
            _isInitializer = isInitializer;
        }
        public int arity()
        {
            return _declaration.param.Count;
        }

        public object call(Interpreter interpreter, List<object> arguments)
        {
            Environment environment = new Environment(_closure);

            for (int i = 0; i < _declaration.param.Count; i++) {
                environment.define(_declaration.param[i].Lexeme,arguments[i]);
            }

            try
            {
                interpreter.executeBlock(_declaration.body, environment);
            }
            catch (Return returnvalue)
            {
                if (_isInitializer) return _closure.getAt(0, "this");
                return returnvalue._value;
            }

            if (_isInitializer) return _closure.getAt(0, "this");

            return null;
        }

        public LoxFunction bind(LoxInstance instance)
        {
            Environment environment = new Environment(_closure);
            environment.define("this", instance);
            return new LoxFunction(_declaration, environment,_isInitializer);
        }
    }
}
