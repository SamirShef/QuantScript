public class TernaryExpression : Expression
{
    private readonly Expression _condition;
    private readonly Expression _trueExpr;
    private readonly Expression _falseExpr;

    public TernaryExpression(Expression condition, Expression trueExpr, Expression falseExpr)
    {
        _condition = condition;
        _trueExpr = trueExpr;
        _falseExpr = falseExpr;
    }

    public Value Eval()
    {
        bool result = _condition.Eval().AsDouble() != 0;
        return result ? _trueExpr.Eval() : _falseExpr.Eval();
    }
}