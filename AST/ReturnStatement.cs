public class ReturnStatement : Exception, Statement
{
    private Expression expression;
    private Value result;

    public ReturnStatement (Expression expression)
    {
        this.expression = expression;
    }

    public Value GetValue()
    {
        return result;
    }

    public void Execute()
    {
        result = expression.Eval();
        throw this;
    }
}