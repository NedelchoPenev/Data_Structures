namespace PitFortress.Classes
{
    using PitFortress.Interfaces;

    public class Minion : IMinion
    {
        public Minion(int xCoordinate, int id)
        {
            this.XCoordinate = xCoordinate;
            this.Health = 100;
            this.Id = id;
        }

        public int CompareTo(Minion other)
        {
            return this.Id.CompareTo(other.Id);
        }

        public int Id { get; private set; }

        public int XCoordinate { get; private set; }

        public int Health { get; set; }
    }
}
