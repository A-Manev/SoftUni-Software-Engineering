using System.Text;

namespace Guild
{
    public class Player
    {
        public Player(string name, string @class)
        {
            this.Name = name;
            this.Class = @class;
        }

        public string Name { get; set; }

        public string Class { get; set; }

        public string Rank { get; set; } = "Trial";

        public string Description { get; set; } = "n/a";

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder
                .AppendLine($"Player {this.Name}: {this.Class}")
                .AppendLine($"Rank: {this.Rank}")
                .AppendLine($"Description: {this.Description}");

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
