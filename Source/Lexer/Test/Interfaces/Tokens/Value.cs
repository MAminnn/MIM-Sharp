namespace MiMSharp.Lang.Lexer
{
    public class Val : IToken
    {
        public Val(int column, int line, object value, System.Type type)
        {
            _column = column;
            _line = line;
            _value = value;
            _type = type;
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

        #region Specefic for values 

        #region public :
        public object Type { get => _type; }
        #endregion

        #region private : 
        private object _type;
        #endregion

        #endregion 

    }
}