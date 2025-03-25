public class FunctionDefineStatement : Statement
{
    private string name;
    private List<string> argsName;
    private Statement body;

    public FunctionDefineStatement (string name, List<string> argsName, Statement body)
    {
        this.name = name;
        this.argsName = argsName;
        this.body = body;
    }

    public void Execute()
    {
        Functions.Set(name, new UserDefinedFunction(argsName, body));
    }
}