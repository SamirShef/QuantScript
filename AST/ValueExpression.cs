public class ValueExpression : Expression
{
    private Value value;

    public ValueExpression(double value)
    {
        this.value = new NumberValue(value);
    }

    public ValueExpression(string value)
    {
        this.value = new StringValue(value);
    }

    public Value Eval()
    {
        return value;
    }
}