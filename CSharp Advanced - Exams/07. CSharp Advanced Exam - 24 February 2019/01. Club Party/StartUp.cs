using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubParty
{
    public class StartUp
    {
        public static void Main()
        {
            int capacity = int.Parse(Console.ReadLine());

            string[] input = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            Stack<string> stack = new Stack<string>(input);

            Queue<int> reservations = new Queue<int>();
            Queue<string> rooms = new Queue<string>();

            Dictionary<string, List<int>> halls = new Dictionary<string, List<int>>();

            while (stack.Any())
            {
                int reservationNumber;
                string @string = stack.Pop();

                bool success = int.TryParse(@string, out reservationNumber);

                if (success && rooms.Count == 0)
                {
                    continue;
                }

                if (success)
                {
                    reservations.Enqueue(reservationNumber);
                }
                else
                {
                    rooms.Enqueue(@string);
                    halls.Add(@string, new List<int>());
                }

                while (reservations.Any() && rooms.Any())
                {
                    int currentReservation = reservations.Peek();
                    string currentRoom = rooms.Peek();

                    if (halls[currentRoom].Sum() + currentReservation <= capacity)
                    {
                        halls[currentRoom].Add(currentReservation);
                        reservations.Dequeue();
                    }
                    else
                    {
                        Console.WriteLine($"{currentRoom} -> { string.Join(", ", halls[currentRoom])}");

                        rooms.Dequeue();
                        halls.Remove(currentRoom);

                        if (rooms.Any())
                        {
                            string currentRoomB = rooms.Peek();

                            if (halls[currentRoomB].Sum() + currentReservation <= capacity)
                            {
                                halls[currentRoomB].Add(currentReservation);
                                reservations.Dequeue();
                            }
                        }
                        else
                        {
                            reservations.Dequeue();
                        }
                    }
                }
            }
        }
    }
}
