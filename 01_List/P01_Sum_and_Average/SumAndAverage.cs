namespace P01_Sum_and_Average
{
    using System;
    using System.Linq;

    class SumAndAverage
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Sum=0; Average=0.00");
                return;
            }

            var list = input.Split().Select(int.Parse).ToList();
            var sum = list.Sum();
            var average = list.Average();
            Console.WriteLine("Sum={0}; Average={1:F2}", sum, average);
        }
    }
}
