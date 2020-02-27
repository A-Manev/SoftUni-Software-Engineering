using System.Collections.Generic;

namespace StudentSystem
{
    public class StudentsDatabase
    {
        private Dictionary<string, Student> collection;

        public StudentsDatabase()
        {
            this.collection = new Dictionary<string, Student>();
        }

        public void Add(string name, int age, double grade)
        {
            if (!this.collection.ContainsKey(name))
            {
                Student student = new Student(name, age, grade);

                this.collection[name] = student;
            }
        }

        public Student Find(string name)
        {
            if (this.collection.ContainsKey(name))
            {
                return this.collection[name];
            }

            return null;
        }
    }
}
