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

    public void AddElement(Value element)
    {
        Value[] elements = new Value[this.elements.Length + 1];
        Array.Copy(this.elements, 0, elements, 0, this.elements.Length);
        elements[^1] = element;
        this.elements = elements;
    }

    public void RemoveElement(Value element)
    {
        Value[] elements = new Value[this.elements.Length - 1];
        int removedIndex = -1;
        for (int i = 0; i < this.elements.Length; i++) if (this.elements[i].AsString() == element.AsString())
        {
            removedIndex = i;
            break;
        }
        if (removedIndex == -1) throw new Exception("Element will not be found");
        
        Array.Copy(this.elements, 0, elements, 0, removedIndex);
        Array.Copy(
            sourceArray: this.elements,
            sourceIndex: removedIndex + 1,
            destinationArray: elements,
            destinationIndex: removedIndex,
            length: this.elements.Length - removedIndex - 1
        );

        this.elements = elements;
    }

    public void RemoveAt(int index)
    {
        Value[] elements = new Value[this.elements.Length - 1];
        Array.Copy(this.elements, 0, elements, 0, index);
        Array.Copy(
            sourceArray: this.elements,
            sourceIndex: index + 1,
            destinationArray: elements,
            destinationIndex: index,
            length: this.elements.Length - index - 1
        );
        this.elements = elements;
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