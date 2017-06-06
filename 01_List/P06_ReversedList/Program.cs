using System;

class Program
{
    static void Main(string[] args)
    {
        var list = new ReversedList<int>();
        list.Add(0);
        list.Add(1);
        list.Add(2);
        list.RemoveAt(0);

        Console.WriteLine(list[0]);
    }
}