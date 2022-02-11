using System;
using System.IO;
using System.Collections.Generic;
using MiMSharp.Lang.Lexer.Test.LexerTest.Interfaces;
using System.Text.RegularExpressions;

namespace MiMSharp.Lang.Lexer.Test.LexerTest
{

    class Program
    {
        static StreamReader fr;
        public static void Main(string[] args)
        {

            FileStream fs = new FileStream(Path.Combine(AppContext.BaseDirectory, "Test.txt"), FileMode.Open);
            using (fr = new StreamReader(fs))
            {
                List<Tuple<string,string>> Ids = new List<Tuple<string,string>>();
                List<Tuple<string,string>> Opereators  = new List<Tuple<string,string>>();
                List<Tuple<string,int>> Numbers = new List<Tuple<string,int>>();
                char readedChar = (char)fr.Read();
                while (readedChar != '\uffff')
                {
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
                            Ids.Add(new Tuple<string, string>("ID",word));
                            break;
                        //For opertators
                        case char c when Regex.IsMatch(c.ToString(), "[+=*/-]"):
                            Opereators.Add(new Tuple<string, string>("OP",c.ToString()));
                            readedChar = (char)fr.Read();
                            break;
                        //For numbers
                        case char c when Regex.IsMatch(c.ToString(), "[0-9]"):
                            string number = default;
                            while (Regex.IsMatch(readedChar.ToString(), "[0-9]"))
                            {
                                number += readedChar;
                                readedChar = (char)fr.Read();
                            }
                            Numbers.Add(new Tuple<string, int>("NUM",int.Parse(number)));
                            break;
                        default:
                            readedChar = (char)fr.Read();
                            break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (var item in Ids)
                {
                    System.Console.WriteLine(item.Item1 +" : "+item.Item2);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var item in Opereators)
                {
                    System.Console.WriteLine(item.Item1 +" : "+item.Item2);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (var item in Numbers)
                {
                    System.Console.WriteLine(item.Item1 +" : "+item.Item2);
                }
            }
        }
        // public static bool IsIdentifier(string word) =>
        //     word == Identifiers.defNumber ||
        //     word == Identifiers.defString;
    }
}