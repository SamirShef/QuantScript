public class FunctionalExpression : Expression
{
    private string name;
    private List<Expression> args;

    public FunctionalExpression (string name)
    {
        this.name = name;
        this.args = new List<Expression>();
    }

    public FunctionalExpression (string name, List<Expression> args)
    {
        this.name = name;
        this.args = args;
    }

    public void AddArgument(Expression arg)
    {
        args.Add(arg);
    }

    public Value Eval()
    {
        int size = args.Count;
        Value[] values = new Value[size];
        for (int i = 0; i < size; i++)
        {
            values[i] = args[i].Eval();
        }

        Function func = Functions.Get(name);
        if (func is UserDefinedFunction)
        {
            UserDefinedFunction userDefinedFunction = (UserDefinedFunction)func;
            if (size != userDefinedFunction.GetArgsCount()) throw new Exception("Arguments count mismatch");

            Variables.Push();
            for (int i = 0; i < size; i++)
            {
                Variables.Set(userDefinedFunction.GetArgName(i), values[i]);
            }
            Value result = userDefinedFunction.Execute(values);
            Variables.Pop();
            return result;
        }

        return Functions.Get(name).Execute(values);
    }
}