public class Functions
{
    private static Dictionary<string, Function> functions = InitFunctions();

    private static Dictionary<string, Function> InitFunctions()
    {
        Dictionary<string, Function> funcs = new Dictionary<string, Function>();
        funcs["Length"] = new LenFunction();
        funcs["Add"] = new AddArrayFunction();
        funcs["Remove"] = new RemoveArrayFunction();
        funcs["RemoveAt"] = new RemoveAtArrayFunction();
        return funcs;
    }

    public static bool IsExists(string key)
    {
        return functions.ContainsKey(key);
    }

    public static Function Get(string key)
    {
        if (!IsExists(key)) throw new Exception($"Function \'{key}\' does not exist");
        return functions[key];
    }

    public static void Set(string key, Function value)
    {
        if (!IsExists(key)) functions.Add(key, value);
        else functions[key] = value;
    }

    public class NewArrayFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            return new ArrayValue(args);
        }
    }

    public class AddArrayFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            if (args.Length != 2) throw new Exception($"Two args expected. You are expected {args.Length} arguments");
            if (!(args[0] is ArrayValue)) throw new Exception("First argument must be of type Array");
            ((ArrayValue)args[0]).AddElement(args[1]);
            return new NumberValue(0);
        }
    }

    public class RemoveArrayFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            if (args.Length != 2) throw new Exception($"Two args expected. You are expected {args.Length} arguments");
            if (!(args[0] is ArrayValue)) throw new Exception("First argument must be of type Array");
            ((ArrayValue)args[0]).RemoveElement(args[1]);
            return new NumberValue(0);
        }
    }

    public class RemoveAtArrayFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            if (args.Length != 2) throw new Exception($"Two args expected. You are expected {args.Length} arguments");
            if (!(args[0] is ArrayValue)) throw new Exception("First argument must be of type Array");
            if (DoubleHelper.HasDecimalPart(args[1].AsDouble())) throw new Exception("Second argument must be of type Int");
            ((ArrayValue)args[0]).RemoveAt((int)args[1].AsDouble());
            return new NumberValue(0);
        }
    }

    public class LenFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            if (args.Length != 1) throw new Exception($"One args expected. You are expected {args.Length} arguments");
            if (args[0] is NumberValue || args[0] is StringValue) return new NumberValue(args[0].AsString().Length);
            /* if (args[0] is ArrayValue) */ return new NumberValue(((ArrayValue)args[0]).GetLen());
        }
    }
}