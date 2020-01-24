using System;
using System.Linq;
using System.Collections.Generic;

namespace AverageStudentGrades
{
    class StartUp
    {
        static void Main()
        {
            int numberOfStudents = int.Parse(Console.ReadLine());

            Dictionary<string, List<decimal>> students = new Dictionary<string, List<decimal>>();

            for (int i = 0; i < numberOfStudents; i++)
            {
                string[] input = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string studentName = input[0];
                decimal studentGrade = decimal.Parse(input[1]);

                if (!students.ContainsKey(studentName))
                {
                    students.Add(studentName, new List<decimal>());
                }

                students[studentName].Add(studentGrade);
            }

            //foreach (var (name, grades) in students)
            //{
            //    Console.Write($"{name} -> ");

            //    foreach (var grade in grades)
            //    {
            //        Console.Write($"{grade:F2} ");
            //    }

            //    Console.WriteLine($"(avg: {grades.Average():F2})");
            //}

            foreach (var (name,grades) in students)
            {
                string allGrades =  string.Join(" ", grades.Select(x => x.ToString("0.00")));
                // "0.00" == "F2" == "N2"
                decimal averageGrade = grades.Average();

                Console.WriteLine($"{name} -> {allGrades} (avg: {averageGrade:F2})");
            }
        }
    }
}
