public class Executor
{
    private LibraryLoader _loader = new LibraryLoader();

    public void Execute(BlockStatement program)
    {
        foreach (var statement in program.statements)
        {
            if (statement is UsingDirective usingDirective)
            {
                _loader.Load(usingDirective.Namespace);
            }
            else
            {
                statement.Execute();
            }
        }
    }
}