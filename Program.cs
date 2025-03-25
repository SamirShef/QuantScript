class Program
{
    static void Main()
    {
        string input = File.ReadAllText("H:/MyProjects/QuantScript/Program.qs");
        Lexer lexer = new Lexer(input);
        List<Token> tokens = lexer.Tokenize();

        Statement program = new Parser(tokens).Parse();
        program.Execute();
    }
}