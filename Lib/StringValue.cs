public class StringValue : Value
{
    private string value;

    public StringValue (string value)
    {
        this.value = value;
    }

    public double AsDouble() => double.TryParse(value, out double result) ? result : throw new Exception("Cannot be converted value to string");

    public string AsString() => value;
}