public class ExpressionStatement : Statement
{
    private Expression expression;

    public ExpressionStatement(Expression expression)
    {
        this.expression = expression;
    }

    public void Execute()
    {
        expression.Eval();
    }
}