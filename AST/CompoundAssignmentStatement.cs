public class CompoundAssignmentStatement : Statement
{
    private string _variable;
    private char _operator;
    private Expression _expression;

    public CompoundAssignmentStatement(string variable, char op, Expression expression)
    {
        _variable = variable;
        _operator = op;
        _expression = expression;
    }

    public void Execute()
    {
        Value current = Variables.Get(_variable);
        Value exprValue = _expression.Eval();
        
        double result = current.AsDouble();
        switch (_operator)
        {
            case '+': result += exprValue.AsDouble(); break;
            case '-': result -= exprValue.AsDouble(); break;
            case '*': result *= exprValue.AsDouble(); break;
            case '/': result /= exprValue.AsDouble(); break;
            case '%': result %= exprValue.AsDouble(); break;
        }
        
        Variables.Set(_variable, new NumberValue(result));
    }
}