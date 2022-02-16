namespace MiMSharp.Lang.Lexer
{
    public class Keyword : IToken
    {
        public Keyword(int column, int line, object value)
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