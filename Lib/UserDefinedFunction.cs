public class UserDefinedFunction : Function
{
    private List<string> argsName;
    private Statement body;

    public UserDefinedFunction (List<string> argsName, Statement body)
    {
        this.argsName = argsName;
        this.body = body;
    }
    
    public int GetArgsCount()
    {
        return argsName.Count;
    }

    public string GetArgName(int index)
    {
        if (index < 0 || index >= GetArgsCount()) throw new Exception("ArgumentOutOfRange");
        return argsName[index];
    }

    public Value Execute(params Value[] args)
    {
        try
        {
            body.Execute();
            return new NumberValue(0);
        }
        catch (ReturnStatement rs)
        {
            return rs.GetValue();
        }
    }
}