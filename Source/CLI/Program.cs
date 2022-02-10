namespace MiMSharp;

using System.CommandLine;

internal static class Program
{
    internal static void Main(string[] args)
    {
        var rootCommand = new RootCommand("MiM-Sharp: C# implementation of the MiM-Lang programming language");

        var helpOption = new Option(
            aliases: new[] { "-h", "--help" },
            description: "Display this help message.",
            arity: ArgumentArity.Zero
        );

        var fileArg = new Argument<FileInfo>(
            name: "file-name",
            description: "The name of the file to run"
        );

        rootCommand.AddArgument(fileArg);
        rootCommand.AddOption(helpOption);

        rootCommand.SetHandler((FileInfo file, object help) =>
            {
                Console.WriteLine($"fileArg: {file}");
                Console.WriteLine($"helpOption: {help}");
            }, fileArg, helpOption
        );
        rootCommand.Invoke(args);
    }
}
