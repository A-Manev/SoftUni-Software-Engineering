using System.Collections.Generic;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public StackOfStrings()
            : base()
        {

        }

        public bool IsEmpty()
        {
            if (base.Count == 0)
            {
                return true;
            }

            return false;
        }

        public void AddRange(IEnumerable<string> collection)
        {
            foreach (var element in collection)

                base.Push(element);
        }
    }
}
