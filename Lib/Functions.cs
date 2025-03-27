using System.Text;

public class Functions
{
    private static Dictionary<string, Function> functions = InitFunctions();

    private static Dictionary<string, Function> InitFunctions()
    {
        Dictionary<string, Function> funcs = new Dictionary<string, Function>();
        funcs["sin"] = new SinFunction();
        funcs["cos"] = new CosFunction();
        funcs["echo"] = new EchoFunction();
        funcs["len"] = new LenFunction();
        funcs["input"] = new InputFunction();
        funcs["int"] = new IntConverterFunction();
        funcs["str"] = new StringConverterFunction();
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

    public class SinFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            if (args.Length != 1) throw new Exception($"One args expected. You are expected {args.Length} arguments");
            return new NumberValue(Math.Sin(args[0].AsDouble()));
        }
    }

    public class CosFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            if (args.Length != 1) throw new Exception($"One args expected. You are expected {args.Length} arguments");
            return new NumberValue(Math.Cos(args[0].AsDouble()));
        }
    }

    public class EchoFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            StringBuilder buffer = new StringBuilder();
            foreach (Value arg in args)
            {
                if (arg is VoidValue) continue;
                buffer.Append($"{arg.AsString()} ");
            }
            
            string result = buffer.ToString().Trim();
            if (!string.IsNullOrEmpty(result))
            {
                Console.WriteLine(result);
            }
            return new NumberValue(0);
        }
    }

    public class NewArrayFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            return new ArrayValue(args);
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
    
    public class InputFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            string input = Console.ReadLine();
            input = input != null ? input : "";
            if (double.TryParse(input, out double result)) return new NumberValue(result);
            else return new StringValue(input);
        }
    }

    public class IntConverterFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            if (args.Length != 1) throw new Exception($"One args expected. You are expected {args.Length} arguments");
            if (double.TryParse(args[0].AsString(), out double result)) return new NumberValue(result);
            else throw new Exception("Cannot convert to int");
        }
    }

    public class StringConverterFunction : Function
    {
        public Value Execute(params Value[] args)
        {
            if (args.Length != 1) throw new Exception($"One args expected. You are expected {args.Length} arguments");
            return new StringValue(args[0].AsString());
        }
    }
}