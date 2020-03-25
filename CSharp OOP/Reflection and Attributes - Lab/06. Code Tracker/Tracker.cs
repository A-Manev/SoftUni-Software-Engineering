using System;
using System.Linq;
using System.Reflection;

public class Tracker
{
    public void PrintMethodsByAuthor()
    {
        Type type = typeof(StartUp);
        MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

        foreach (MethodInfo method in methods)
        {
            if (method.CustomAttributes.Any(n => n.AttributeType == typeof(AuthorAttribute)))
            {
                object[] attributes = method.GetCustomAttributes(false);

                foreach (AuthorAttribute author in attributes)
                {
                    Console.WriteLine($"{method.Name} is written by {author.Name}");
                }
            }
        }
    }
}
