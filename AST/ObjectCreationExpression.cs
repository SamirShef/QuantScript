public class ObjectCreationExpression : Expression
{
    private readonly string _className;

    public ObjectCreationExpression(string className)
    {
        _className = className;
    }

    public Value Eval()
    {
        var obj = new ObjectValue();
        var classDecl = Classes.Get(_className);
        
        // Устанавливаем контекст "this" для инициализации полей
        Variables.Push();
        Variables.Set("this", obj);
        classDecl.Body.Execute();
        Variables.Pop();

        return obj;
    }
}