public class MemberAssignmentStatement : Statement
{
    private readonly MemberAccessExpression _memberAccess;
    private readonly Expression _expression;

    public MemberAssignmentStatement(MemberAccessExpression memberAccess, Expression expression)
    {
        _memberAccess = memberAccess;
        _expression = expression;
    }

    public void Execute()
    {
        Value value = _expression.Eval();
        _memberAccess.SetValue(value);
    }
}