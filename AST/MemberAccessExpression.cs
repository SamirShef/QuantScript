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
                return Functions.Get(_memberName).Execute();

            throw new Exception($"Member '{_memberName}' not found");
        }

        throw new Exception("Member access on non-object value");
    }
}