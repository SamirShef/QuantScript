public class ArrayAssignmentStatement : Statement
{
    private string variable;
    private Expression index;
    private Expression expression;

    public ArrayAssignmentStatement (string variable, Expression index, Expression expression)
    {
        this.variable = variable;
        this.index = index;
        this.expression = expression;
    }

    public void Execute()
    {
        Value var = Variables.Get(variable);
        if (var is ArrayValue)
        {
            ArrayValue array = (ArrayValue)var;
            array.Set((int)index.Eval().AsDouble(), expression.Eval());
        }
        else throw new Exception("Array Expected");
    }
}