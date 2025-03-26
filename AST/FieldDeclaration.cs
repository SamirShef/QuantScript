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
        var thisObj = Variables.Get("this") as ObjectValue;
        if (thisObj == null) 
            throw new Exception("'this' is not defined");
        
        Value value = (initializer != null) ? initializer.Eval() : new NumberValue(0);
        thisObj.SetField(name, value);
    }
}