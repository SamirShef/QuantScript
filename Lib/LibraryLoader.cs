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
}