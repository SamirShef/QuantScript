using QSystem

func WriteAllElements(array)
{
    for (var i = 0; i < Length(array); i++) Console.Write(Convert.ToString(array[i]) + " ")
    Console.WriteLine()
}

var array = [1, 2, 2, 4, 5, 6, 7, 8, 9]
WriteAllElements(array)
Add(array, 10)
WriteAllElements(array)
Remove(array, 2)
WriteAllElements(array)
RemoveAt(array, 3)
WriteAllElements(array)