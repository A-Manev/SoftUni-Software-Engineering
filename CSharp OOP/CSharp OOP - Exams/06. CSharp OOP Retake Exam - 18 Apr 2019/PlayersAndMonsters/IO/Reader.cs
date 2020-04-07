namespace PlayersAndMonsters.IO
{
    using System;

    using PlayersAndMonsters.IO.Contracts;

    public class Reader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
