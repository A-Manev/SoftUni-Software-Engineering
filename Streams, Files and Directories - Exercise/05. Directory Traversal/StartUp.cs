using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace DirectoryTraversal
{
    class StartUp
    {
        static void Main()
        {
            string inputDirectory = Console.ReadLine(); // подайте му само " . "

            string[] files = Directory.GetFiles(inputDirectory);

            Dictionary<string, Dictionary<string, double>> dictionary = new Dictionary<string, Dictionary<string, double>>();

            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                if (!dictionary.ContainsKey(fileInfo.Extension))
                {
                    dictionary.Add(fileInfo.Extension, new Dictionary<string, double>());
                }

                dictionary[fileInfo.Extension].Add(fileInfo.Name, fileInfo.Length);
            }

            ///ПЕЧАТА НА КОНЗОЛАТА
           
            //foreach (var extension in dictionary.OrderByDescending(k => k.Value.Count).ThenBy(name => name.Key))
            //{
            //    Console.WriteLine(extension.Key);

            //    foreach (var (name, size) in extension.Value.OrderBy(size => size.Value))
            //    {
            //        double currentSize = size / 1024;
            //        Console.WriteLine($"--{name} - {currentSize:F3}kb");
            //    }
            //}

            //using StreamWriter writer = new StreamWriter("../../../report.txt"); /// създава файла в текущата директория

            ///run visual studio as administrator - иначе не позволява и пише "Access to the path 'C:\Users\Public\Desktop\report.txt' is denied."

            using StreamWriter writer = new StreamWriter(@"C:/Users/Public/Desktop/report.txt");

            foreach (var extension in dictionary.OrderByDescending(k => k.Value.Count).ThenBy(name => name.Key))
            {
                writer.WriteLine(extension.Key);

                foreach (var (name, size) in extension.Value.OrderBy(size => size.Value))
                {
                    double currentSize = size / 1024;

                    writer.WriteLine($"--{name} - {currentSize:F3}kb");
                }
            }
        }
    }
}
