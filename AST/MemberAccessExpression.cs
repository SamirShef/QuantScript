public class MemberAccessExpression : Expression
{
    private readonly Expression _target;
    private readonly string _memberName;
    private readonly List<Expression> _arguments;

    public MemberAccessExpression(Expression target, string memberName, List<Expression> arguments = null)
    {
        _target = target;
        _memberName = memberName;
        _arguments = arguments ?? new List<Expression>();
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
            var method = obj.GetMethod(_memberName);
            if (method != null)
            {
                // Вычисляем аргументы
                Value[] args = new Value[_arguments.Count];
                for (int i = 0; i < _arguments.Count; i++)
                {
                    args[i] = _arguments[i].Eval();
                }

                Variables.Push();
                Variables.Set("this", obj);
                Value result = method.Execute(args);
                Variables.Pop();
                return result;
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