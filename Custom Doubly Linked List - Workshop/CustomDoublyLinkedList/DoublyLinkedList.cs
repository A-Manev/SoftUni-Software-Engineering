using System;

namespace CustomDoublyLinkedList
{
    public partial class StartUp
    {
        public class DoublyLinkedList
        {
            private ListNode head;
            private ListNode tail;

            public int Count { get; private set; }

            public void AddFirst(int element)
            {
                if (this.Count == 0)
                {
                    this.head = this.tail = new ListNode(element);
                }
                else
                {
                    ListNode newHead = new ListNode(element);
                    ListNode oldHead = this.head;

                    this.head = newHead;
                    this.head.NextNode = oldHead;
                    oldHead.PreviousNode = newHead;
                }

                this.Count++;
            }

            public void AddLast(int element)
            {
                if (this.Count == 0)
                {
                    this.head = this.tail = new ListNode(element);
                }
                else
                {
                    ListNode newTail = new ListNode(element);
                    ListNode oldTail = this.tail;

                    this.tail = newTail;
                    newTail.PreviousNode = oldTail;
                    oldTail.NextNode = newTail;
                }

                this.Count++;
            }

            public int RemoveFirst()
            {
                if (this.Count == 0)
                {
                    throw new InvalidOperationException("The list is empty");
                }

                int removedElement = this.head.Value;

                if (this.Count == 1)
                {
                    this.head = null;
                    this.tail = null;
                }
                else
                {
                    ListNode newHead = this.head.NextNode;

                    this.head = newHead;
                    newHead.PreviousNode = null;
                }

                this.Count--;

                return removedElement;
            }

            public int RemoveLast()
            {
                if (this.Count == 0)
                {
                    throw new InvalidOperationException("The list is empty");
                }

                int removedElement = this.tail.Value;

                if (this.Count == 0)
                {
                    this.head = null;
                    this.tail = null;
                }
                else
                {
                    ListNode newTail = this.tail.PreviousNode;

                    this.tail = newTail;
                    newTail.NextNode = null;

                }

                this.Count--;
                return removedElement;
            }

            public void ForEach(Action<int> action)
            {
                ListNode currentElement = this.head;

                while (currentElement != null)
                {
                    action(currentElement.Value);
                    currentElement = currentElement.NextNode;
                }
            }

            public int[] ToArray()
            {
                int[] array = new int[this.Count];
                int counter = 0;

                ListNode currentElement = this.head;

                while (currentElement != null)
                {
                    array[counter] = currentElement.Value;
                    currentElement = currentElement.NextNode;
                    counter++;
                }

                return array;
            }
        }
    }
}
