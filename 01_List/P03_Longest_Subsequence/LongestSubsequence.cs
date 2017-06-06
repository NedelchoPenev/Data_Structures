namespace P03_Longest_Subsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class LongestSubsequence
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToList();
            
            List<int> longest = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                List<int> current = new List<int>();
                current.Add(input[i]);

                for (int j = i + 1; j < input.Count; j++)
                {
                    if (input[i] == input[j])
                    {
                        current.Add(input[j]);
                    }

                    else
                    {
                        break;
                    }
                }

                if (current.Count > longest.Count)
                {
                    longest = current;
                }
            }

            Console.WriteLine(string.Join(" ", longest));

        }
    }
}
