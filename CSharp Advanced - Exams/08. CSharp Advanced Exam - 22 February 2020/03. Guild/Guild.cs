using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Guild
{
    public class Guild
    {
        private List<Player> roster;

        public Guild(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.roster = new List<Player>();
        }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Count => this.roster.Count;

        public void AddPlayer(Player player)
        {
            if (this.roster.Count + 1 <= this.Capacity)
            {
                this.roster.Add(player);
            }
        }

        public bool RemovePlayer(string name)
        {
            var target = this.roster.FirstOrDefault(x => x.Name == name);

            if (target != null)
            {
                this.roster.Remove(target);

                return true;
            }

            return false;
        }

        public void PromotePlayer(string name)
        {
            var target = this.roster.First(x => x.Name == name);

            if (target.Rank != "Member")
            {
                target.Rank = "Member";
            }
        }

        public void DemotePlayer(string name)
        {
            var target = this.roster.First(x => x.Name == name);

            if (target.Rank != "Trial")
            {
                target.Rank = "Trial";
            }
        }

        public Player[] KickPlayersByClass(string @class)
        {
            Player[] players = this.roster.Where(x => x.Class == @class).ToArray();

            this.roster.RemoveAll(x => x.Class == @class);

            return players;
        }

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Players in the guild: {this.Name}");

            foreach (var player in this.roster)
            {
                stringBuilder.AppendLine(player.ToString());
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
