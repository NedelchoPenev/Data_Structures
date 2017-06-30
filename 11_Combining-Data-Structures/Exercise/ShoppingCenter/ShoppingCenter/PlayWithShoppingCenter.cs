using System;

class Program
{
    static void Main(string[] args)
    {
        var lines = int.Parse(Console.ReadLine());

        var center = new ShoppingCenter();
        for (int i = 0; i < lines; i++)
        {
            var input = Console.ReadLine();
            var index = input.IndexOf(' ');

            var command = input.Substring(0, index);
            var cmdArgs = input.Substring(index + 1).Split(';');

            switch (command)
            {
                case "AddProduct":
                    Console.WriteLine(center.AddProduct(cmdArgs[0],decimal.Parse(cmdArgs[1]), cmdArgs[2]));
                    break;
                case "DeleteProducts":
                    Console.WriteLine(
                        cmdArgs.Length == 1
                            ? center.DeleteProducts(cmdArgs[0])
                            : center.DeleteProducts(cmdArgs[0], cmdArgs[1]));
                    break;
                case "FindProductsByName":
                    Console.Write(center.FindProductsByName(cmdArgs[0]));
                    break;
                case "FindProductsByProducer":
                    Console.Write(center.FindProductsByProducer(cmdArgs[0]));
                    break;
                case "FindProductsByPriceRange":
                    Console.Write(center.FindProductsByPriceRange(decimal.Parse(cmdArgs[0]), decimal.Parse(cmdArgs[1])));
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}