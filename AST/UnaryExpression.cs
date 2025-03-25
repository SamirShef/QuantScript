public class UnaryExpression : Expression
{
    private Expression expr1;
    private char operation;

    public UnaryExpression (char operation, Expression expr1)
    {
        this.operation = operation;
        this.expr1 = expr1;
    }

    public Value Eval()
    {
        switch (operation)
        {
            case '+': return expr1.Eval();
            case '-': return new NumberValue(-expr1.Eval().AsDouble());
            default: return new NumberValue(0);
        }
    }
}