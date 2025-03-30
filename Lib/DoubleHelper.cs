public static class DoubleHelper
{
    private const double Epsilon = 1e-9; // Точность сравнения

    public static bool HasDecimalPart(double number)
    {
        // Проверка на специальные случаи: NaN, бесконечность
        if (double.IsNaN(number) || double.IsInfinity(number))
            return false;

        // Получаем целую часть числа
        double integerPart = Math.Truncate(number);
        
        // Сравниваем разницу с учетом погрешности
        return Math.Abs(number - integerPart) > Epsilon;
    }
}