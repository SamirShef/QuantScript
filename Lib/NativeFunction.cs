using System.Reflection;
using System.Linq;

public class NativeFunction : Function
{
    private readonly MethodInfo _method;
    private readonly object?[] _defaultArgs;

    public NativeFunction(MethodInfo method)
    {
        _method = method;
        _defaultArgs = method.GetParameters()
            .Select(p => p.DefaultValue)
            .ToArray();
    }

    public Value Execute(params Value[] args)
    {
        try
        {
            var parameters = _method.GetParameters();
            object?[] finalArgs = new object?[parameters.Length];

            // Заполняем переданные аргументы
            for (int i = 0; i < args.Length; i++)
            {
                finalArgs[i] = ConvertArgument(args[i]);
            }

            // Добавляем значения по умолчанию для не указанных аргументов
            for (int i = args.Length; i < parameters.Length; i++)
            {
                finalArgs[i] = _defaultArgs[i];
            }

            // Вызов метода
            var result = _method.Invoke(null, finalArgs);
            return ConvertResult(result);
        }
        catch (TargetInvocationException ex) // Перехватываем исключения из C#
        {
            // Извлекаем оригинальное исключение и его сообщение
            throw new Exception($"{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка вызова метода: {ex.Message}");
        }
    }

    private object ConvertArgument(Value arg)
    {
        return arg switch
        {
            NumberValue num => num.AsDouble(),
            StringValue str => str.AsString(),
            ArrayValue arr => ConvertArray(arr),
            _ => throw new Exception("Неподдерживаемый тип аргумента")
        };
    }

    private object[] ConvertArray(ArrayValue arr)
    {
        var list = new List<object>();
        for (int i = 0; i < arr.GetLen(); i++)
        {
            list.Add(ConvertArgument(arr.Get(i)));
        }
        return list.ToArray();
    }

    private Value ConvertResult(object result)
    {
        return result switch
        {
            double d => new NumberValue(d),
            int i => new NumberValue(i),
            string s => new StringValue(s),
            IEnumerable<object> enumerable => new ArrayValue(enumerable.Select(ConvertResult).ToArray()),
            _ => throw new Exception($"Неподдерживаемый тип результата: {result.GetType()}")
        };
    }
}