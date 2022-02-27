using System;
using System.Collections.Generic;
namespace MiMSharp.Lang.Lexer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            using (System.IO.StreamReader reader = new System.IO.StreamReader("O:\\Developing\\Projects\\Productions\\MIM-Sharp\\MiM-Sharp\\MIM-Sharp\\Source\\Lexer\\Formatter\\Demo.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();
                    while (row[0] == ' ')
                    {
                        row = row.TrimStart(' ');
                    }
                    if (row.Contains("="))
                    {
                        while (!row.Contains(" = "))
                        {
                            row = row.Replace("="," = ");
                        }
                    }
                    while (row.Contains("  ") || row.Contains("   "))
                    {
                        row = row.Replace("  ", " ");
                        row = row.Replace("   ", " ");
                    }
                    lines.Add(row);
                }
            }
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("O:\\Developing\\Projects\\Productions\\MIM-Sharp\\MiM-Sharp\\MIM-Sharp\\Source\\Lexer\\Formatter\\Demo.txt"))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}