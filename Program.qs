class AWD
{
    var awd = 4

    func Multiply(a, b)
    {
        return a * b
    }

    func Print(a, b)
    {
        echo(a, b)
        echo("ADWAD")
    }
}

class Calculator {
    var awd = new AWD()
}

var calc = new Calculator()
echo(calc.awd.awd)
echo(calc.awd.Multiply(2, 3))
echo(calc.awd.Print(2, 3))