using System;
using System.Collections.Generic;
using System.Collections;
namespace MiMSharp.Lang.Lexer
{
    public struct Statics
    {
        //Publics:
        public static char[] Operators
        {
            get => new char[] { '=', '-', '+', '/', '*' };
        }

        public static string[] Keywords
        {
            //var => define variable | if => define condition | in => define loop | ref => refrence to a module
            get => new string[] { "var", "if", "in", "ref" };
        }
    }
}