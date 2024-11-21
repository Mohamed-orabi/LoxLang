
namespace LoxLang
{
    public class LoxFunction : LoxCallable
    {
        private readonly Stmt.Function _declaration;

        public LoxFunction(Stmt.Function declaration)
        {
            _declaration = declaration;
        }
        public int arity()
        {
            return _declaration.param.Count;
        }

        public object call(Interpreter interpreter, List<object> arguments)
        {
            Environment environment = new Environment(interpreter.globals);

            for (int i = 0; i < _declaration.param.Count; i++) {
                environment.define(_declaration.param[i].Lexeme,arguments[i]);
            }

            try
            {
                interpreter.executeBlock(_declaration.body, environment);
            }
            catch (Return returnvalue)
            {
                return returnvalue._value;
            }
            
            return null;
        }
    }
}
