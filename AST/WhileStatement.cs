public class WhileStatement : Statement
{
    private Expression condition;
    private Statement statement;

    public WhileStatement (Expression condition, Statement statement)
    {
        this.condition = condition;
        this.statement = statement;
    }

    public void Execute()
    {
        while (condition.Eval().AsDouble() != 0)
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
    }
}