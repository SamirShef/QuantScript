public class IF_Statement : Statement
{
    private Expression expression;
    private Statement if_Statement, else_Statement;

    public IF_Statement (Expression expression, Statement if_Statement, Statement else_Statement)
    {
        this.expression = expression;
        this.if_Statement = if_Statement;
        this.else_Statement = else_Statement;
    }

    public void Execute()
    {
        double result = expression.Eval().AsDouble();
        if (result != 0) if_Statement.Execute();
        else if (else_Statement != null) else_Statement.Execute();
    }
}