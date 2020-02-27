using System;

namespace StudentSystem
{
    public class ConsoleIoEngine : IIoEngine
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string str)
        {
            Console.WriteLine(str);
        }
    }
}
