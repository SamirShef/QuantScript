public class ObjectCreationExpression : Expression
{
    private string className;
    
    public ObjectCreationExpression(string className)
    {
        this.className = className;
    }
    
    public Value Eval()
    {
        // Создаем экземпляр объекта
        var obj = new ObjectValue();
        Variables.Set("this", obj); // Для работы с "this" внутри методов
        
        // Инициализируем поля класса
        var classDecl = Classes.Get(className);
        classDecl.Body.Execute();
        
        return obj;
    }
}