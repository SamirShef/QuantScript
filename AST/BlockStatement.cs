public class BlockStatement : Statement
{
    public List<Statement> statements;

    public BlockStatement ()
    {
        statements = new List<Statement>();
    }

    public void Add(Statement statement)
    {
        statements.Add(statement);
    }

    public void Execute()
    {
        foreach (Statement statement in statements)
        {
            statement.Execute();
        }
    }
}