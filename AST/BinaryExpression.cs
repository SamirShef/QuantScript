using System.Text;

public class BinaryExpression : Expression
{
    private Expression expr1, expr2;
    private char operation;

    public BinaryExpression (char operation, Expression expr1, Expression expr2)
    {
        this.operation = operation;
        this.expr1 = expr1;
        this.expr2 = expr2;
    }

    public Value Eval()
    {
        Value value1 = expr1.Eval();
        Value value2 = expr2.Eval();
        if (value1 is StringValue || value1 is ArrayValue)
        {
            string str1 = value1.AsString();
            string str2 = value2.AsString();
            switch (operation)
            {
                case '+': return new StringValue(str1 + str2);
                case '*':
                {
                    int iterations = (int)value2.AsDouble();
                    StringBuilder buffer = new StringBuilder();
                    for (int i = 0; i < iterations; i++)
                    {
                        buffer.Append(str1);
                    }
                    return new StringValue(buffer.ToString());
                }
                default: return new NumberValue(0);
            }
        }

        double num1 = value1.AsDouble();
        double num2 = value2.AsDouble();
        
        switch (operation)
        {
            case '+': return new NumberValue(num1 + num2);
            case '-': return new NumberValue(num1 - num2);
            case '*': return new NumberValue(num1 * num2);
            case '/': return new NumberValue(num1 / num2);
            default: return new NumberValue(0);
        }
    }
}