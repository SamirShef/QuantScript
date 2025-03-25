public class StringValue : Value
{
    private string value;

    public StringValue (string value)
    {
        this.value = value;
    }

    public double AsDouble()
    {
        if (double.TryParse(value, out double result))
        {
            return result;
        }
        else
        {
            throw new Exception("Cannot be converted value to string");
        }
    }

    public string AsString()
    {
        return value;
    }
}