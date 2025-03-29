public class IncrementDecrementStatement : Statement
{
    private string _variable;
    private bool _isIncrement;

    public IncrementDecrementStatement(string variable, bool isIncrement)
    {
        _variable = variable;
        _isIncrement = isIncrement;
    }

    public void Execute()
    {
        Value current = Variables.Get(_variable);
        if (current is NumberValue)
        {
            double value = current.AsDouble();
            value += _isIncrement ? 1 : -1;
            Variables.Set(_variable, new NumberValue(value));
        }
        else
        {
            string incrementable = _isIncrement ? "incremental" : "decremental";
            throw new Exception($"Type \'{current.GetType()}\' cannot be {incrementable}");
        }
    }
}