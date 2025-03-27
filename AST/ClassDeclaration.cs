public class ClassDeclaration : Statement
{
    public string Name { get; }
    public BlockStatement Body { get; }

    public ClassDeclaration(string name, BlockStatement body)
    {
        Name = name;
        Body = body;
    }

    public void Execute()
    {
        var tempObj = new ObjectValue();
        Variables.Push();
        Variables.Set("this", tempObj);
        Body.Execute();
        Variables.Pop();

        // Переносим методы в класс
        foreach (var method in tempObj.Methods)
        {
            tempObj.SetMethod(method.Key, method.Value);
        }
        
        Classes.Set(Name, this);
    }
}