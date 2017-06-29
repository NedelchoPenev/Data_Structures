namespace PitFortress.Classes
{
    using System;

    using PitFortress.Interfaces;

    public class Player : IPlayer
    {
        public Player(string name, int radius)
        {
            this.Name = name;
            this.Radius = radius;
        }

        public int CompareTo(Player other)
        {
            int result = other.Score.CompareTo(this.Score);
            if (result == 0)
            {
                result = String.Compare(other.Name, this.Name, StringComparison.Ordinal);
            }

            return result;
        }

        public string Name { get; private set; }

        public int Radius { get; private set; }

        public int Score { get; set; }
    }
}
