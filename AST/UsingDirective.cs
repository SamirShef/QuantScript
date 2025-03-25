public class UsingDirective : Statement
{
    public string Namespace { get; }

    public UsingDirective(string Namespace) 
    {
        this.Namespace = Namespace;
    }

    public void Execute()
    {

    }
}