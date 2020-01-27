using System;
using System.Collections.Generic;
using System.Linq;

namespace Ranking
{
    class StartUp
    {
        static void Main()
        {
            Dictionary<string, string> contests = new Dictionary<string, string>();

            string input = Console.ReadLine();
            input = InitializeContests(contests, input);

            List<Student> students = new List<Student>();

            input = Console.ReadLine();

            while (input != "end of submissions")
            {
                string[] inputInfo = input
                    .Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string contestsName = inputInfo[0];
                string contestsPassword = inputInfo[1];
                string username = inputInfo[2];
                int point = int.Parse(inputInfo[3]);

                if (contests.ContainsKey(contestsName) && contests[contestsName] == contestsPassword)
                {
                    Student student = new Student(username);

                    if (!students.Any(x => x.Name == username))
                    {
                        students.Add(student);
                        var currentStudent = students.FirstOrDefault(x => x.Name == username);
                        currentStudent.Contests.Add(contestsName, point);
                    }
                    else
                    {
                        var currentStudent = students.FirstOrDefault(x => x.Name == username);

                        if (!currentStudent.Contests.Any(x => x.Key == contestsName))
                        {
                            currentStudent.Contests.Add(contestsName, point);
                        }
                        else
                        {
                            if (currentStudent.Contests.Any(x => x.Value < point))
                            {
                                var currentPoints = currentStudent.Contests.FirstOrDefault(x => x.Key == contestsName);

                                if (currentPoints.Value < point)
                                {
                                    currentStudent.Contests.Remove(contestsName);
                                    currentStudent.Contests.Add(contestsName, point);
                                }
                            }
                        }
                    }
                }

                input = Console.ReadLine();
            }

            PrintBestCandidate(students);

            PrintSudentsRankingList(students);
        }

        private static void PrintSudentsRankingList(List<Student> students)
        {
            Console.WriteLine("Ranking:");

            foreach (var student in students.OrderBy(x => x.Name))
            {
                Console.WriteLine(student.Name);

                foreach (var item in student.Contests.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"#  {item.Key} -> {item.Value}");
                }
            }
        }

        private static void PrintBestCandidate(List<Student> students)
        {
            string bestStudent = string.Empty;
            int maxPoint = int.MinValue;

            foreach (var student in students)
            {
                int points = 0;

                foreach (var point in student.Contests)
                {
                    points += point.Value;

                    if (maxPoint < points)
                    {
                        maxPoint = points;
                        bestStudent = student.Name;
                    }
                }
            }

            Console.WriteLine($"Best candidate is {bestStudent} with total {maxPoint} points.");
        }

        private static string InitializeContests(Dictionary<string, string> contests, string input)
        {
            while (input != "end of contests")
            {
                string[] inputContests = input
                    .Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string contestsName = inputContests[0];
                string contestsPassword = inputContests[1];

                if (!contests.ContainsKey(contestsName))
                {
                    contests.Add(contestsName, contestsPassword);
                }

                input = Console.ReadLine();
            }

            return input;
        }

        public class Student
        {
            public Student(string name)
            {
                Name = name;
            }

            public string Name { get; set; }

            public Dictionary<string, int> Contests { get; set; } = new Dictionary<string, int>();
        }
    }
}
