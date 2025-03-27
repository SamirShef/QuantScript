public class ObjectValue : Value
{
    public Dictionary<string, Value> Fields { get; } = new Dictionary<string, Value>();
    public Dictionary<string, Function> Methods { get; } = new();

    public double AsDouble() => throw new Exception("Object cannot be converted to number");
    public string AsString() => $"Object@{GetHashCode()}";

    public void SetField(string name, Value value) => Fields[name] = value;
    public Value GetField(string name) => Fields.TryGetValue(name, out var val) ? val : throw new Exception($"Field '{name}' not found");
    public void SetMethod(string name, Function method) => Methods[name] = method;
    public Function GetMethod(string name) => Methods.TryGetValue(name, out var method) ? method : null;
}