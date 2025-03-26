public class UserDefinedMethod : Function
{
    private List<string> parameters;
    private Statement body;
    private bool isVoid;

    public UserDefinedMethod(List<string> parameters, Statement body, bool isVoid)
    {
        this.parameters = parameters;
        this.body = body;
        this.isVoid = isVoid;
    }

    public Value Execute(params Value[] args)
    {
        Variables.Push();
        try
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                Variables.Set(parameters[i], i < args.Length ? args[i] : new NumberValue(0));
            }

            body.Execute();
            return isVoid ? new NumberValue(0) : Variables.Get("result");
        }
        finally
        {
            Variables.Pop();
        }
    }
}