public class FunctionStatement : Statement
{
    private FunctionalExpression function;

    public FunctionStatement (FunctionalExpression function)
    {
        this.function = function;
    }

    public void Execute()
    {
        function.Eval();
    }
}