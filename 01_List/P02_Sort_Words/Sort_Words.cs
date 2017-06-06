namespace P02_Sort_Words
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Sort_Words
    {
        static void Main(string[] args)
        {
            List<string> input = Console.ReadLine().Split().ToList();
            input.Sort();

            Console.WriteLine(string.Join(" ", input));
        }
    }
}
