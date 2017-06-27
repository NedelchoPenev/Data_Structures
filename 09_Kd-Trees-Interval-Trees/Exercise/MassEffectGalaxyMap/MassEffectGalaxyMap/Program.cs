using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int starClusters = int.Parse(Console.ReadLine());
        int reportsMake = int.Parse(Console.ReadLine());
        int galaxySize  = int.Parse(Console.ReadLine());

        Rectangle galaxy = new Rectangle(0, 0, galaxySize, galaxySize);
        KdTree universe = new KdTree(galaxy);

        for (int i = 0; i < starClusters; i++)
        {
            var star = Console.ReadLine().Split();
            string name = star[0];
            double x = double.Parse(star[1]);
            double y = double.Parse(star[2]);

            Point2D coordinates = new Point2D(x, y);
            universe.Insert(coordinates);
        }

        for (int i = 0; i < reportsMake; i++)
        {
            var report = Console.ReadLine().Split();
            double x1 = double.Parse(report[1]);
            double y1 = double.Parse(report[2]);
            double x2 = double.Parse(report[3]);
            double y2 = double.Parse(report[4]);

            Rectangle searchedPart = new Rectangle(x1, y1, x2, y2);

            int starsInSearchedPart = universe.RangeSearch(searchedPart);
            Console.WriteLine(starsInSearchedPart);
        }
    }
}