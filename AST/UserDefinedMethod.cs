public class UserDefinedMethod : Function
{
    private List<string> parameters;
    private Statement body;
    private bool isVoid;

    public UserDefinedMethod(List<string> parameters, Statement body, bool isVoid)
    {
        this.parameters = parameters;
        this.body = body;
        this.isVoid = isVoid;
    }

    public Value Execute(params Value[] args)
    {
        Variables.Push();
        try
        {
            var thisObj = Variables.Get("this") as ObjectValue;
            
            // Устанавливаем параметры
            for (int i = 0; i < parameters.Count; i++)
            {
                Variables.Set(parameters[i], i < args.Length ? args[i] : new NumberValue(0));
            }

            // Выполняем тело метода
            body.Execute();

            // Для void-методов возвращаем 0, для функций - ожидаем return
            if (isVoid) return new NumberValue(0);
            
            // Если функция не void, но return не вызван - ошибка
            throw new Exception("Function must return a value");
        }
        catch (ReturnStatement rs)
        {
            // Возвращаем значение из return
            return rs.GetValue();
        }
        finally
        {
            Variables.Pop();
        }
    }
}