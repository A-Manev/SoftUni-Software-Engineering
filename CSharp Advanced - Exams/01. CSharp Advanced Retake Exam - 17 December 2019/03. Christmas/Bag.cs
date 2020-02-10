using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Christmas
{
    public class Bag
    {
        private List<Present> data;

        public Bag(string color, int capacity)
        {
            this.Color = color;
            this.Capacity = capacity;
            this.data = new List<Present>();
        }

        public int Count => this.data.Count;

        public string Color { get; set; }

        public int Capacity { get; set; }

        public void Add(Present present)
        {
            if (this.Capacity != 0 && !data.Contains(present))
            {
                data.Add(present);
            }
        }

        public bool Remove(string name)
        {
            var target = data.FirstOrDefault(x => x.Name == name);

            if (target == null)
            {
                return false;
            }

            data.Remove(target);

            return true;
        }

        public Present GetHeaviestPresent()
        {
            int maxWeight = int.MinValue;

            Present heaviestPresent = null;

            foreach (var present in data)
            {
                if (present.Weight > maxWeight)
                {
                    heaviestPresent = present;
                }
            }

            return heaviestPresent;
        }

        public Present GetPresent(string name)
        {
            Present targetPresent = data.First(x => x.Name == name);

            return targetPresent;
        }

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder
                .AppendLine($"{this.Color} bag contains:");

            foreach (var present in data)
            {
                stringBuilder.AppendLine(present.ToString());
            }
            return stringBuilder.ToString().TrimEnd();
        }
    }
}
