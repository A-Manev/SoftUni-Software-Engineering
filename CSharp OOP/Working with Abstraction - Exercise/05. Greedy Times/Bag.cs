using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace GreedyTimes
{
    public class Bag
    {
        private List<Item> bag;
        private long totalCapacity;
        private long currentCapacity;

        public Bag(long capacity)
        {
            this.totalCapacity = capacity;
            this.bag = new List<Item>();
        }

        public long GoldItemsValue => this.bag.Where(i => i is Gold).Sum(i => i.Value);

        public long GemItemsValue => this.bag.Where(i => i is Gem).Sum(i => i.Value);

        public long CashItemsValue => this.bag.Where(i => i is Cash).Sum(i => i.Value);

        public List<Item> GetGoldItems() => this.bag.Where(i => i is Gold).ToList();

        public List<Item> GetGemItems() => this.bag.Where(i => i is Gem).ToList();

        public List<Item> GetCashItems() => this.bag.Where(i => i is Cash).ToList();

        public void AddGoldItem(Gold item)
        {
            if (this.totalCapacity >= this.currentCapacity + item.Value)
            {
                List<Item> goldItems = this.GetGoldItems();

                if (!goldItems.Any(g => g.Key == item.Key))
                {
                    this.bag.Add(item);
                }
                else
                {
                    goldItems.Single(g => g.Key == item.Key).IncreaseValue(item.Value);
                }

                this.currentCapacity += item.Value;
            }
        }

        public void AddGemItem(Gem item)
        {
            if (this.totalCapacity >= this.currentCapacity + item.Value &&
                this.GoldItemsValue >= this.GemItemsValue + item.Value)
            {
                List<Item> gemItems = this.GetGemItems();

                if (!gemItems.Any(g => g.Key == item.Key))
                {
                    this.bag.Add(item);
                }
                else
                {
                    gemItems.Single(g => g.Key == item.Key).IncreaseValue(item.Value);
                }

                this.currentCapacity += item.Value;
            }
        }

        public void AddCashItem(Cash item)
        {
            if (this.totalCapacity >= this.currentCapacity + item.Value &&
                this.GemItemsValue >= this.CashItemsValue + item.Value)
            {
                List<Item> cashItems = this.GetCashItems();

                if (!cashItems.Any(c => c.Key == item.Key))
                {
                    this.bag.Add(item);
                }
                else
                {
                    cashItems.Single(c => c.Key == item.Key).IncreaseValue(item.Value);
                }

                this.currentCapacity += item.Value;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            Dictionary<string, List<Item>> dictionary = this.bag
                .GroupBy(i => i.GetType().Name)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var kvp in dictionary.OrderByDescending(x => x.Value.Sum(i => i.Value)))
            {
                if (kvp.Key == "Gold")
                {
                    stringBuilder.AppendLine($"<Gold> ${kvp.Value.Sum(i => i.Value)}");
                }
                else if (kvp.Key == "Gem")
                {
                    stringBuilder.AppendLine($"<Gem> ${kvp.Value.Sum(i => i.Value)}");
                }
                else if (kvp.Key == "Cash")
                {
                    stringBuilder.AppendLine($"<Cash> ${kvp.Value.Sum(i => i.Value)}");
                }

                foreach (var item in kvp.Value.OrderByDescending(i => i.Key).ThenBy(i => i.Value))
                {
                    stringBuilder.AppendLine($"##{item.Key} - {item.Value}");
                }
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}