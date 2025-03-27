class Calculator {
    var memory = 5

    void Sum(a, b)
    {
        return a + b
    }

    void Print(a)
    {
        echo(a)
    }
}

var calc = new Calculator()
echo(calc.memory) // 5
calc.memory = 1 // переприсвоит переменную класса
echo(calc.memory) // 1
echo(calc.Sum(1, 2)) // 3
calc.Print(2) // 2