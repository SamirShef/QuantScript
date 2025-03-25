public class ForStatement : Statement
{
    private Statement initialization;
    private Expression termination;
    private Statement increment;
    private Statement block;

    public ForStatement (Statement initialization, Expression termination, Statement increment, Statement block)
    {
        this.initialization = initialization;
        this.termination = termination;
        this.increment = increment;
        this.block = block;
    }

    public void Execute()
    {
        for (initialization.Execute(); termination.Eval().AsDouble() != 0; increment.Execute())
        {
            try
            {
                block.Execute();
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