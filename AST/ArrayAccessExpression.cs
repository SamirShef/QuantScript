public class ArrayAccessExpression : Expression
{
    private string variable;
    private Expression index;

    public ArrayAccessExpression (string variable, Expression index)
    {
        this.variable = variable;
        this.index = index;
    }

    public Value Eval()
    {
        Value var = Variables.Get(variable);
        if (var is ArrayValue)
        {
            ArrayValue array = (ArrayValue)var;
            return array.Get((int)index.Eval().AsDouble());
        }
        else throw new Exception("Array Expected");
    }
}