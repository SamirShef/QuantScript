public class FieldDeclaration : Statement
{
    private string name;
    private Expression initializer;

    public FieldDeclaration(string name, Expression initializer)
    {
        this.name = name;
        this.initializer = initializer;
    }

    public void Execute()
    {
        Value value = initializer?.Eval() ?? new NumberValue(0);
        Variables.CurrentContext[name] = value;
    }
}