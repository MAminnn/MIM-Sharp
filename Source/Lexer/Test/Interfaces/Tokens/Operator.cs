namespace MiMSharp.Lang.Lexer
{
    public class Operator : IToken
    {
        public Operator(int column, int line, object value)
        {
            _column = column;
            _line = line;
            _value = value;
        }

        #region IToken implementation

        #region public :
        public int Column { get => _column; }
        public int Line { get => _line; }
        public object Value { get => _value; }
        #endregion

        #region private :
        private int _column;
        private int _line;
        private object _value;
        #endregion

        #endregion

    }
}