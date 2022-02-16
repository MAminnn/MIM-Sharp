using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiMSharp.Lang.Lexer
{

    public class Program
    {
        public static void Main(string[] args)
        {
            Lexer lexer = new Lexer(Path.Combine(AppContext.BaseDirectory, "Test.txt"));
            IEnumerable<IToken> Tokens = lexer.Lex();

            foreach (IToken token in Tokens)
            {
                switch (token)
                {
                    case Keyword:
                        System.Console.WriteLine($"Found a Keyword at {token.Line}:{token.Column} => {token.Value}");
                        break;
                    case Identifier:
                        System.Console.WriteLine($"Found an Identifier at {token.Line}:{token.Column} => {token.Value}");
                        break;
                    case Val:
                        System.Console.WriteLine($"Found a Value at {token.Line}:{token.Column} => {token.Value} | Type: {((Val)token).Type}");
                        break;
                    case Operator:
                        System.Console.WriteLine($"Found an Operator at {token.Line}:{token.Column} => {token.Value}");
                        break;
                }

            }
        }
    }

    public class Lexer
    {
        private string _inputStream;
        private static List<IToken> Tokens = new List<IToken>();
        public Lexer(string inputStream)
        {
            _inputStream = inputStream;
        }
        public IEnumerable<IToken> Lex()
        {
            using (FileStream fs = new FileStream(_inputStream, FileMode.Open))
            {
                using (StreamReader fr = new StreamReader(fs))
                {
                    char readedChar = (char)fr.Read();
                    int line = 1;
                    int lastcol = 1;
                    while (readedChar != '\uffff')
                    {
                        // Token token = new Token();
                        if (readedChar == '\r')
                        {

                            line++;
                            lastcol = 1;
                            readedChar = (char)fr.Read();
                        }
                        switch (readedChar)
                        {
                            //For keywords
                            case char c when Regex.IsMatch(c.ToString(), "[a-zA-Z]"):
                                string word = default;
                                while (Regex.IsMatch(readedChar.ToString(), "[a-zA-Z]"))
                                {
                                    word += readedChar;
                                    readedChar = (char)fr.Read();
                                }

                                if (IsKeyword(word))
                                {
                                    Tokens.Add(new Keyword(lastcol, line, word));
                                }
                                else
                                {
                                    Tokens.Add(new Identifier(lastcol, line, word));
                                }
                                lastcol += word.Length;
                                break;
                            //For opertators
                            case char c when Regex.IsMatch(c.ToString(), "[+=*/-]"):
                                Tokens.Add(new Operator(lastcol, line, c.ToString()));
                                lastcol += c.ToString().Length;
                                readedChar = (char)fr.Read();
                                break;
                            //For numbers
                            case char c when Regex.IsMatch(c.ToString(), "[0-9]"):
                                string number = default;
                                while (Regex.IsMatch(readedChar.ToString(), "[0-9]") || readedChar == '.')
                                {
                                    number += readedChar;
                                    readedChar = (char)fr.Read();
                                }
                                if (Regex.IsMatch(number, @"[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)") && number.Contains("."))
                                {
                                    Tokens.Add(new Val(lastcol, line, float.Parse(number), typeof(Single)));
                                }
                                else if (Regex.IsMatch(number, "[0-9]"))
                                {
                                    Tokens.Add(new Val(lastcol, line, int.Parse(number), typeof(int)));
                                }
                                else
                                {
                                    throw new Exception($"Unknown number type at {line}:{lastcol} ");
                                }
                                lastcol += number.Length;
                                break;
                            case ' ':
                                readedChar = (char)fr.Read();
                                lastcol += 1;
                                break;

                            default:
                                if (readedChar != '\n')
                                    throw new Exception($"Unknown syntax at {line}:{lastcol} ");
                                else
                                    readedChar = (char)fr.Read();
                                break;
                        }
                    }
                }
            }

            return Tokens;
        }

        public bool IsKeyword(string word)
        {
            foreach (var Keyword in Statics.Keywords)
            {
                if (word == Keyword)
                    return true;
            }
            return false;
        }


    }
}