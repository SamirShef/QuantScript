using QSystem

var a = 10 > 5 ? "Yes" : "No"
Console.WriteLine(a) // Выведет "Yes"

var x = Console.ReadLine()
var b = (x == 0) ? 100 : (x > 0 ? 1 : -1)
Console.WriteLine(b)