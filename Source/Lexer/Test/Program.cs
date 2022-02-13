using System;
using System.IO;
using System.Collections.Generic;
using MiMSharp.Lang.Lexer.Test.LexerTest.Interfaces;
using System.Text.RegularExpressions;

namespace MiMSharp.Lang.Lexer.Test.LexerTest
{

    public class Program
    {

        public static void Main(string[] args)
        {
            Lexer lexer = new Lexer(Path.Combine(AppContext.BaseDirectory, "Test.txt"));
            IEnumerable<Tuple<string, string, int, int>> TokensizesList = lexer.Lex();

            foreach (Tuple<string, string, int, int> item in TokensizesList)
            {
                Console.WriteLine($"{item.Item1} => {item.Item2} at {item.Item3}:{item.Item4}");
            }
        }
    }

    public class Lexer
    {
        private string _inputStream;
        private static List<Tuple<string, string, int, int>> TokensizesList = new List<Tuple<string, string, int, int>>();
        public Lexer(string inputStream)
        {
            _inputStream = inputStream;
        }
        public IEnumerable<Tuple<string, string, int, int>> Lex()
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
                                    TokensizesList.Add(new Tuple<string, string, int, int>("KW_VAR", word, line, lastcol));
                                else
                                    TokensizesList.Add(new Tuple<string, string, int, int>("ID", word, line, lastcol));

                                lastcol += word.Length;
                                break;
                            //For opertators
                            case char c when Regex.IsMatch(c.ToString(), "[+=*/-]"):
                                TokensizesList.Add(new Tuple<string, string, int, int>("OP", c.ToString(), line, lastcol));
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
                                    TokensizesList.Add(new Tuple<string, string, int, int>("FLT", float.Parse(number).ToString(), line, lastcol));
                                }
                                else if(Regex.IsMatch(number,"[0-9]"))
                                {
                                    TokensizesList.Add(new Tuple<string, string, int, int>("NUM", number, line, lastcol));
                                }
                                else{
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

            return TokensizesList;
        }

        public bool IsKeyword(string word) =>
    word == Keywords.defNumber ||
    word == Keywords.defString;
    }
}