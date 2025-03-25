public class NumberValue : Value
{
    private double value;

    public NumberValue (double value)
    {
        this.value = value;
    }

    public NumberValue (bool value)
    {
        this.value = value ? 1 : 0;
    }

    public double AsDouble()
    {
        return value;
    }

    public string AsString()
    {
        return value.ToString();
    }
}