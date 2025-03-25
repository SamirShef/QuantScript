public class ContinueStatement : Exception, Statement
{
    public void Execute()
    {
        throw this;
    }
}