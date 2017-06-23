using System;

class Program
{
    static void Main(string[] args)
    {
        var interpreter = new CommandInterpreter();
        while (true)
        {
            var input = Console.ReadLine().Split();
            if (input[0].Equals("end"))
            {
                break;
            }

            string command = input[0];
            switch (command)
            {
                case "add":
                    string name = input[1];
                    int x1 = int.Parse(input[2]);
                    int y1 = int.Parse(input[3]);
                    interpreter.AddObject(name, x1, y1);
                    break;
                case "start":
                    interpreter.StartGame();
                    break;
                case "tick":
                    interpreter.Tick();
                    PrintColides(interpreter);
                    break;
                case "move":
                    string nameM = input[1];
                    int x1M = int.Parse(input[2]);
                    int y1M = int.Parse(input[3]);
                    interpreter.MoveObject(nameM, x1M, y1M);
                    PrintColides(interpreter);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    private static void PrintColides(CommandInterpreter interpreter)
    {
        var collided = interpreter.SweepAndPrune();
        for (int i = 0; i < collided.Count; i += 2)
        {
            Console.WriteLine($"({interpreter.Ticks}) {collided[i].Name} collides with {collided[i + 1].Name}");
        }
    }
}