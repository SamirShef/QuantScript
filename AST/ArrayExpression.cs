public class ArrayExpression : Expression
{
    private List<Expression> elements;

    public ArrayExpression (List<Expression> elements)
    {
        this.elements = elements;
    }

    public Value Eval()
    {
        int size = elements.Count;
        ArrayValue array = new ArrayValue(size);
        for (int i = 0; i < size; i++) array.Set(i, elements[i].Eval());
        return array;
    }
}