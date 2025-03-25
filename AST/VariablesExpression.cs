public class VariablesExpression : Expression
{
    public string name;

    public VariablesExpression (string name)
    {
        this.name = name;
    }
    public Value Eval()
    {
        if (!Variables.IsExists(name)) throw new Exception($"Constant \'{name}\' does not exist");
        return Variables.Get(name);
    }
}