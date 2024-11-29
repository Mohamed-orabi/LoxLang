using static LoxLang.Stmt;

namespace LoxLang
{
    public class LoxInstance
    {
        private readonly LoxClass _klass;

        public LoxInstance(LoxClass klass)
        {
            _klass = klass;
        }

        public override string ToString()
        {
            return _klass._name + " instance";
        }
    }
}
