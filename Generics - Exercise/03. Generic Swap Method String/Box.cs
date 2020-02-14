using System.Text;
using System.Collections.Generic;

namespace GenericSwapMethodString
{
    public class Box<T>
    {
        private List<T> data;

        public Box()
        {
            this.data = new List<T>();
        }

        public void Add(T item)
        {
            this.data.Add(item);
        }

        public void Swap(int indexOne, int indexTwo)
        {
            T current = this.data[indexTwo];
            this.data[indexTwo] = this.data[indexOne];
            this.data[indexOne] = current;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var currentElement in this.data)
            {
                stringBuilder.AppendLine($"{currentElement.GetType()}: {currentElement}");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
