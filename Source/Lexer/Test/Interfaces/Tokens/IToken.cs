namespace MiMSharp.Lang.Lexer
{
    public interface IToken
    {
        int Column { get; }
        
        int Line { get; }
        
        object Value { get; }

    }
}