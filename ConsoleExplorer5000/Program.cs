class Program
{
    static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        var consoleExplorer5000 = new ConsoleExplorer5000();
        consoleExplorer5000.Run();
    }
}
