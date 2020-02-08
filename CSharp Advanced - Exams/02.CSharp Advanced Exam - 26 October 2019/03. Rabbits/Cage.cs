using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rabbits
{
    public partial class StartUp
    {
        public class Cage
        {
            private List<Rabbit> data;

            public Cage(string name, int capacity)
            {
                Name = name;
                Capacity = capacity;
                data = new List<Rabbit>();
            }

            public string Name { get; set; }

            public int Capacity { get; set; }

            public int Count => this.data.Count;

            public void Add(Rabbit rabbit)
            {
                if (this.Capacity > 0 && !data.Any(x=>x.Name == rabbit.Name))
                {
                    data.Add(rabbit);
                }
            }//TODO "!data.Any(x=>x.Name == rabbit.Name)"

            public bool RemoveRabbit(string name)
            {
                var targetRabbit = data.FirstOrDefault(x => x.Name == name);

                if (targetRabbit == null)
                {
                    return false;
                }

                return true;
            }

            public void RemoveSpecies(string species)
            {
                data.RemoveAll(x => x.Species == species);
            }

            public Rabbit SellRabbit(string name)
            {
                var targetRabbit = data.First(x => x.Name == name);

                targetRabbit.Available = false;

                return targetRabbit;
            }

            public Rabbit[] SellRabbitsBySpecies(string species)
            {
                Rabbit[] rabbits = data.Where(x => x.Species == species).ToArray();

                foreach (var rabbit in rabbits)
                {
                    rabbit.Available = false;
                }

                return rabbits;
            }

            public string Report()
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendLine($"Rabbits available at {this.Name}:");

                foreach (var rabbit in data)
                {
                    if (rabbit.Available == true)
                    {
                        stringBuilder.AppendLine(rabbit.ToString());

                    }
                }
                return stringBuilder.ToString().TrimEnd();
            }
        }
    }
}
