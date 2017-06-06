namespace P05_Count_of_Occurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class CountOfOccurrences
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToList();

            var result = new SortedDictionary<int,int>();

            for (int i = 0; i < input.Count; i++)
            {
                var occurence = input.Count(n => n == input[i]);
                if (!result.ContainsKey(input[i]))
                {
                    result.Add(input[i], occurence);
                }
            }

            foreach (var keyValuePair in result)
            {
                Console.WriteLine("{0} -> {1} times", keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}
