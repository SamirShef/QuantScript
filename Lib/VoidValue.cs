public class VoidValue : Value
{
    public double AsDouble() => 0;
    public string AsString() => null; // Возвращаем null вместо пустой строки
}