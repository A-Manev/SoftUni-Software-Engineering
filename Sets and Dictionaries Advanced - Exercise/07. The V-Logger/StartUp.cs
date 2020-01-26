using System;
using System.Linq;
using System.Collections.Generic;

namespace TheVLogger
{
    class StartUp
    {
        static void Main()
        {
            List<Vlogger> records = new List<Vlogger>();

            string inputCommand = Console.ReadLine();

            while (inputCommand != "Statistics")
            {
                string[] commandInfo = inputCommand
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                if (commandInfo.Length == 4)
                {
                    string vloggerName = commandInfo[0];

                    if (!records.Any(x => x.Name == vloggerName))
                    {
                        Vlogger user = new Vlogger(vloggerName);

                        records.Add(user);
                    }
                }
                else
                {
                    string followerName = commandInfo[0];
                    string followingName = commandInfo[2];

                    var follower = records.FirstOrDefault(x => x.Name == followerName);
                    var following = records.FirstOrDefault(x => x.Name == followingName);

                    if (follower != null && following != null && follower != following)
                    {
                        follower.Following.Add(followingName);
                        following.Followers.Add(followerName);
                    }
                }

                inputCommand = Console.ReadLine();
            }

            Console.WriteLine($"The V-Logger has a total of {records.Count} vloggers in its logs.");

            PrintVloggers(records);
        }

        private static void PrintVloggers(List<Vlogger> records)
        {
            int count = 1;

            foreach (var user in records
                .OrderByDescending(x => x.Followers.Count)
                .ThenBy(x => x.Following.Count))
            {
                Console.WriteLine($"{count}. {user.Name} : {user.Followers.Count} followers, {user.Following.Count} following");

                if (count == 1)
                {
                    foreach (var folloers in user.Followers.OrderBy(x => x))
                    {
                        Console.WriteLine($"*  {folloers}");
                    }
                }

                count++;
            }
        }

        //public class User
        //{
        //    public User(string name)
        //    {
        //        Name = name;
        //    }

        //    public string Name { get; set; }

        //    public HashSet<string> Followers { get; set; } = new HashSet<string>();

        //    public HashSet<string> Following { get; set; } = new HashSet<string>();
        //}

        public class Vlogger
        {
            private HashSet<string> followers;
            private HashSet<string> following;
            
            public Vlogger(string name)
            {
                Name = name;
                followers = new HashSet<string>();
                following = new HashSet<string>();
            }

            public string Name { get; set; }

            public HashSet<string> Followers => followers;

            public HashSet<string> Following => following;
        }
    }
}
