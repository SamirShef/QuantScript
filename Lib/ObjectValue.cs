public class ObjectValue : Value
{
    public Dictionary<string, Value> Fields { get; } = new Dictionary<string, Value>();
    
    public double AsDouble() => throw new InvalidCastException("Object cannot be converted to number");
    public string AsString() => $"Object instance of {GetType().Name}";
    
    public void SetField(string name, Value value) => Fields[name] = value;
    public Value GetField(string name) => Fields.TryGetValue(name, out var val) ? val : throw new Exception($"Field '{name}' not found");
}