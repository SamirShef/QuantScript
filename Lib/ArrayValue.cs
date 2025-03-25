public class ArrayValue : Value
{
    private Value[] elements;

    public ArrayValue (Value[] elements)
    {
        this.elements = new Value[elements.Length];
        for (int i = 0; i < elements.Length; i++) this.elements[i] = elements[i];
    }

    public ArrayValue (int size)
    {
        this.elements = new Value[size];
    }

    public ArrayValue (ArrayValue array) : this(array.elements)
    {
    }

    public int GetLen()
    {
        return elements.Length;
    }

    public Value Get(int index)
    {
        if (index < 0 || index >= elements.Length) throw new Exception("IndexOutOfRange");
        return elements[index];
    }

    public void Set(int index, Value value)
    {
        if (index < 0 || index >= elements.Length) throw new Exception("IndexOutOfRange");
        elements[index] = value;
    }

    public double AsDouble()
    {
        throw new Exception("Cannot cast array to number");
    }

    public string AsString()
    {
        return elements != null ? elements.ToString() : "";
    }
}