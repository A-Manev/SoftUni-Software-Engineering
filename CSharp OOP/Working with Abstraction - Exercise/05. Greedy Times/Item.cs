namespace GreedyTimes
{
    public abstract class Item
        {
            public string Key { get; protected set; }

            public long Value { get; protected set; }

            public void IncreaseValue(long value)
            {
                this.Value += value;
            }
        }
}
