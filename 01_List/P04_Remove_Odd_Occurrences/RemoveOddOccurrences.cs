namespace P04_Remove_Odd_Occurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class RemoveOddOccurrences
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToList();

            var result = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                var occurence = input.Count(n => n == input[i]);
                if (occurence % 2 == 0)
                {
                    result.Add(input[i]);
                }
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
