using System;
using System.Collections.Generic;

namespace GenericCountMethodString
{
    public class Box<T> where T : IComparable
    {
        public Box()
        {
            this.Values = new List<T>();
        }

        public List<T> Values { get; set; }

        public int CountGreaterElements(T elementToCompare)
        {
            int counter = 0;

            foreach (var element in Values)
            {
                if (element.CompareTo(elementToCompare) > 0)
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
