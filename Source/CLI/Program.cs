namespace MiMSharp.Lang.CLI;

using System.CommandLine;
using MiMSharp.Lang.Lexer;

internal static class Program
{
    internal static void Main(string[] args)
    {
        var rootCommand = new RootCommand("MiM-Sharp: C# implementation of the MiM-Lang programming language");

        var fileArg = new Argument<string>(
            name: "file-name",
            description: "The name of the file to run"
        );

        rootCommand.AddArgument(fileArg);

        rootCommand.SetHandler((string fileName) => Execute(fileName), fileArg);
        rootCommand.Invoke(args);
    }

    private static void Execute(string fileName)
    {
        if (!File.Exists(fileName)) {
            Console.WriteLine($"\u001b[31m\0file '{fileName}' does not exist.\u001b[0m");
            return;
        }

        Lexer lexer = new Lexer(fileName);
        lexer.Lex();
    }
}
