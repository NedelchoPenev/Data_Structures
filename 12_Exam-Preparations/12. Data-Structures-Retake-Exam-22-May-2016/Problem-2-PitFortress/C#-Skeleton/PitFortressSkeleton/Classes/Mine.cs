namespace PitFortress.Classes
{
    using PitFortress.Interfaces;

    public class Mine : IMine
    {
        public Mine(Player player, int xCoordinate, int delay, int damage, int id)
        {
            this.Id = id;
            this.Delay = delay;
            this.Damage = damage;
            this.XCoordinate = xCoordinate;
            this.Player = player;
        }

        public int CompareTo(Mine other)
        {
            int result = this.Delay.CompareTo(other.Delay);
            if (result == 0)
            {
                result = this.Id.CompareTo(other.Id);
            }

            return result;
        }

        public int Id { get; private set; }

        public int Delay { get; set; }

        public int Damage { get; private set; }

        public int XCoordinate { get; private set; }

        public Player Player { get; private set; }
    }
}
