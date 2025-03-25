public class DoWhileStatement : Statement
{
    private Expression condition;
    private Statement statement;

    public DoWhileStatement (Expression condition, Statement statement)
    {
        this.condition = condition;
        this.statement = statement;
    }

    public void Execute()
    {
        do
        {
            try
            {
                statement.Execute();
            }
            catch (BreakStatement)
            {
                break;
            }
            catch (ContinueStatement)
            {
                continue;
            }
        }
        while (condition.Eval().AsDouble() != 0);
    }
}