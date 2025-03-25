public class Variables
{
    private static Dictionary<string, Value> variables = InitConstants();
    public static Dictionary<string, Value> CurrentContext => variables;
    private static Stack<Dictionary<string, Value>> stack = new Stack<Dictionary<string, Value>>();

    private static Dictionary<string, Value> InitConstants()
    {
        Dictionary<string, Value> vars = new Dictionary<string, Value>();
        stack = new Stack<Dictionary<string, Value>>();
        vars.Add("PI", new NumberValue(MathF.PI));
        vars.Add("E", new NumberValue(MathF.E));
        return vars;
    }

    public static void Push()
    {
        stack.Push(new Dictionary<string, Value>(variables));
    }

    public static void Pop()
    {
        variables = stack.Pop();
    }

    public static bool IsExists(string key)
    {
        return variables.ContainsKey(key);
    }

    public static Value Get(string key)
    {
        if (!IsExists(key)) return new NumberValue(0);
        return variables[key];
    }

    public static void Set(string key, Value value)
    {
        if (!IsExists(key)) variables.Add(key, value);
        else variables[key] = value;
    }
}