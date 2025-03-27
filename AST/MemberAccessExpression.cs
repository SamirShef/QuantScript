public class MemberAccessExpression : Expression
{
    private readonly Expression _target;
    private readonly string _memberName;

    public MemberAccessExpression(Expression target, string memberName)
    {
        _target = target;
        _memberName = memberName;
    }

    public Value Eval()
    {
        var targetValue = _target.Eval();
        
        if (targetValue is ObjectValue obj)
        {
            // Доступ к полю
            if (obj.Fields.TryGetValue(_memberName, out var field))
                return field;

            // Вызов метода
            if (Functions.IsExists(_memberName))
            {
                Function func = Functions.Get(_memberName);
                if (func is UserDefinedMethod method)
                {
                    // Передаем объект как контекст
                    Variables.Push();
                    Variables.Set("this", obj);
                    Value result = method.Execute();
                    Variables.Pop();
                    return result;
                }
                return func.Execute();
            }

            throw new Exception($"Member '{_memberName}' not found");
        }

        throw new Exception("Member access on non-object value");
    }

    public void SetValue(Value value)
    {
        var targetValue = _target.Eval();
        if (targetValue is ObjectValue obj)
        {
            obj.SetField(_memberName, value);
            return;
        }
        throw new Exception("Member access on non-object value");
    }
}