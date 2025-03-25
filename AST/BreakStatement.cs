public class BreakStatement : Exception, Statement
{
    public void Execute()
    {
        throw this;
    }
}