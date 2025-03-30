using System.Reflection;
using System.IO;

public class LibraryLoader
{
    private string _libsPath = Path.Combine(Directory.GetCurrentDirectory(), "Libs");

    public void Load(string libraryName)
    {
        try
        {
            string dllPath = Path.Combine(_libsPath, $"{libraryName}.dll");
            var assembly = Assembly.LoadFrom(dllPath);
            
            foreach (var type in assembly.GetTypes())
            {
                // Создаем объект для статического класса
                var staticClassObj = new ObjectValue();
                
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
                {
                    // Добавляем метод в объект класса
                    staticClassObj.SetMethod(method.Name, new NativeFunction(method));
                }

                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    object value = field.GetValue(null); // Получаем значение поля
                    staticClassObj.SetField(field.Name, ConvertToValue(value));
                }
                
                // Регистрируем класс в переменных
                Variables.Set(type.Name, staticClassObj);
            }
        }
        catch (FileNotFoundException)
        {
            throw new Exception($"Библиотека '{libraryName}' не найдена в папке Libs.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка загрузки библиотеки '{libraryName}': {ex.Message}");
        }
    }

    private Value ConvertToValue(object obj)
    {
        return obj switch
        {
            double d => new NumberValue(d),
            int i => new NumberValue(i),
            string s => new StringValue(s),
            _ => throw new Exception($"Unsupported field type: {obj?.GetType()}")
        };
    }
}