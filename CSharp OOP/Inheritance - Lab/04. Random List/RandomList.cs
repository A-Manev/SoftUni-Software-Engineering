using System;
using System.Collections.Generic;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        public RandomList()
            : base()
        {

        }

        public string RandomString()
        {
            Random random = new Random();

            int index = random.Next(0, base.Count);
            string element = base[index];
            base.RemoveAt(index);

            return element;
        }
    }
}
