public class MethodDeclaration : Statement
{
    public string Name { get; }
    public List<string> Parameters { get; }
    public Statement Body { get; }
    public bool IsVoid { get; }

    public MethodDeclaration(string name, List<string> parameters, Statement body, bool isVoid)
    {
        Name = name;
        Parameters = parameters;
        Body = body;
        IsVoid = isVoid;
    }

    public void Execute()
    {
        var function = new UserDefinedMethod(Parameters, Body, IsVoid);
        Functions.Set(Name, function);
    }
}