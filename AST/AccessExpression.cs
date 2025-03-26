public class AccessExpression : Expression
{
    private Expression obj;
    private string member;
    
    public AccessExpression(Expression obj, string member)
    {
        this.obj = obj;
        this.member = member;
    }
    
    public Value Eval()
    {
        var instance = obj.Eval() as ObjectValue;
        return instance.Fields[member];
    }
}