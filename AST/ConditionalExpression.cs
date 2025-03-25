public class ConditionalExpression : Expression
{
    public enum Operator
    {
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,

        EQUALS,
        NOT_EQUALS,

        LT,
        LTEQ,
        GT,
        GTEQ,

        AND,
        OR
    }

    private Expression expr1, expr2;
    private Operator operation;

    public ConditionalExpression (Operator operation, Expression expr1, Expression expr2)
    {
        this.operation = operation;
        this.expr1 = expr1;
        this.expr2 = expr2;
    }

    public Value Eval()
    {
        Value value1 = expr1.Eval();
        Value value2 = expr2.Eval();

        double num1, num2;
        if (value1 is StringValue)
        {
            num1 = value1.AsString().CompareTo(value2.AsString());
            num2 = 0;
        }
        else
        {
            num1 = value1.AsDouble();
            num2 = value2.AsDouble();
        }
        
        bool result;
        switch (operation)
        {
            case Operator.EQUALS: result = num1 == num2; break;
            case Operator.NOT_EQUALS: result = num1 != num2; break;
            case Operator.GT: result = num1 > num2; break;
            case Operator.GTEQ: result = num1 >= num2; break;
            case Operator.LT: result = num1 < num2; break;
            case Operator.LTEQ: result = num1 <= num2; break;

            case Operator.AND: result = num1 != 0 && num2 != 0; break;
            case Operator.OR: result = num1 != 0 || num2 != 0; break;
            default: result = false; break;
        }
        return new NumberValue(result);
    }
}