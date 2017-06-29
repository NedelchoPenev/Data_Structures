namespace PitFortress
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PitFortress.Classes;
    using PitFortress.Interfaces;

    using Wintellect.PowerCollections;

    public class PitFortressCollection : IPitFortress
    {
        private Dictionary<string, Player> players;

        private OrderedMultiDictionary<int, Minion> minions;

        private SortedSet<Mine> mines;

        private SortedSet<Player> topPlayers;

        private int minionsId;
        private int mineId;

        public PitFortressCollection()
        {
            this.players = new Dictionary<string, Player>();
            this.minions = new OrderedMultiDictionary<int, Minion>(false);
            this.mines = new SortedSet<Mine>();
            this.topPlayers = new SortedSet<Player>();

            this.minionsId = 1;
            this.mineId = 1;
        }

        public int PlayersCount => this.players.Count;

        public int MinionsCount => this.minions.Values.Count;

        public int MinesCount => this.mines.Count;

        public void AddPlayer(string name, int mineRadius)
        {
            if (this.players.ContainsKey(name) || mineRadius < 0)
            {
                throw new ArgumentException();
            }

            var newPlayer = new Player(name, mineRadius);
            this.players[name] = newPlayer;
            this.topPlayers.Add(newPlayer);
        }

        public void AddMinion(int xCoordinate)
        {
            if (xCoordinate > 1_000_000 || xCoordinate < 0)
            {
                throw new ArgumentException();
            }

            var newMinion = new Minion(xCoordinate, this.minionsId++);
            this.minions[xCoordinate].Add(newMinion);
        }

        public void SetMine(string playerName, int xCoordinate, int delay, int damage)
        {
            if (!this.players.ContainsKey(playerName) || 
                (xCoordinate > 1_000_000 || xCoordinate < 0) ||
                (delay > 10000 || delay < 0) ||
                damage > 100 || damage < 0)
            {
                throw new ArgumentException();
            }

            var player = this.players[playerName];
            var newMine = new Mine(player, xCoordinate, delay, damage, this.mineId++);
            this.mines.Add(newMine);
        }

        public IEnumerable<Minion> ReportMinions()
        {
            return this.minions.Values;
        }

        public IEnumerable<Player> Top3PlayersByScore()
        {
            if (this.topPlayers.Count < 3)
            {
                throw new ArgumentException();
            }

            return this.topPlayers.Take(3);
        }

        public IEnumerable<Player> Min3PlayersByScore()
        {
            if (this.topPlayers.Count < 3)
            {
                throw new ArgumentException();
            }

            return this.topPlayers.Reverse().Take(3);
        }

        public IEnumerable<Mine> GetMines()
        {
            return this.mines;
        }

        public void PlayTurn()
        {
            foreach (var mine in this.mines)
            {
                mine.Delay--;
            }

            var closestToExplosion = this.mines.Min;
            while (closestToExplosion != null && closestToExplosion.Delay == 0)
            {
                this.Explode(closestToExplosion);
                this.mines.Remove(closestToExplosion);
                closestToExplosion = this.mines.Min;
            }
        }

        private void Explode(Mine closestToExplosion)
        {
            var startOfExplodion = closestToExplosion.XCoordinate - closestToExplosion.Player.Radius;
            var endOfExplodion = closestToExplosion.XCoordinate + closestToExplosion.Player.Radius;

            var range = this.minions.Range(startOfExplodion, true, endOfExplodion, true).Values.ToList();
            foreach (var minion in range)
            {
                minion.Health -= closestToExplosion.Damage;
                if (minion.Health <= 0)
                {
                    this.minions.Remove(minion.XCoordinate);
                    var player = closestToExplosion.Player;
                    this.topPlayers.Remove(player);
                    player.Score++;
                    this.topPlayers.Add(player);
                }
            }
        }
    }
}