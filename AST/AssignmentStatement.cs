public class AssignmentStatement : Statement
{
    private string variable;
    private Expression expression;

    public AssignmentStatement (string variable, Expression expression)
    {
        this.variable = variable;
        this.expression = expression;
    }

    public void Execute()
    {
        Value result = expression.Eval();
        Variables.Set(variable, result);
    }
}