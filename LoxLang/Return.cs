namespace LoxLang
{
    public class Return : Exception
    {
        public readonly object _value;

        public Return(object value) : base() 
        {
            _value = value;
        }
    }
}
