namespace StudentSystem
{
    class CreateCommand : ICommand
    {
        public void Execute(string[] args, StudentsDatabase database)
        {
            string name = args[1];
            int age = int.Parse(args[2]);
            double grade = double.Parse(args[3]);

            database.Add(name, age, grade);
        }
    }
}
