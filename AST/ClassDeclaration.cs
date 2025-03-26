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
        Classes.Set(Name, this);
    }
}