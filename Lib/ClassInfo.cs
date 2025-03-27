public class ClassInfo
{
    public string Name { get; }
    public BlockStatement Body { get; }

    public ClassInfo(string name, BlockStatement body)
    {
        Name = name;
        Body = body;
    }

    public ObjectValue CreateInstance()
    {
        var obj = new ObjectValue();
        Variables.Push();
        Variables.Set("this", obj);
        Body.Execute(); // Инициализация полей и методов
        Variables.Pop();
        return obj;
    }
}