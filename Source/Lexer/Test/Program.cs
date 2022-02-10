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

            FileStream fs = new FileStream(Path.Combine(AppContext.BaseDirectory,"Test.txt"), FileMode.Open);
            using (fr = new StreamReader(fs))
            {
                List<string> ids = new List<string>();
                List<string> names = new List<string>();
                List<string> operators = new List<string>();
                List<int> numbers = new List<int>();
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
                            if (IsIdentifier(word: word))
                                InsertId(ids: ids, id: word);
                            else
                                InsertName(names: names, name: word);
                            break;
                        //For opertators
                        case char c when Regex.IsMatch(c.ToString(), "[+=*/-]"):
                            operators.Add(c.ToString());
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
                            numbers.Add(int.Parse(number));
                            break;
                        default:
                            readedChar = (char)fr.Read();
                            break;
                    }

                }
                foreach (string id in ids)
                {
                    Console.WriteLine("Found an identifier : " + id);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*********************************");
                foreach (string name in names)
                {
                    Console.WriteLine("Found a name : " + name);
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("*********************************");
                foreach (string op in operators)
                {
                    Console.WriteLine("Found an operator : " + op);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*********************************");
                foreach (int number in numbers)
                {
                    Console.WriteLine("Found a number : " + number);
                }
            }
        }
        public static bool IsIdentifier(string word) =>
            word == Identifiers.defNumber ||
            word == Identifiers.defString;
        public static void InsertId(List<string> ids, string id) => ids.Add(id);

        public static void InsertName(List<string> names, string name) => names.Add(name);
    }
}