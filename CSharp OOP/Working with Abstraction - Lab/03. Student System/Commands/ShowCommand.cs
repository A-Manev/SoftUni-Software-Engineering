using System;

namespace StudentSystem
{
    public class ShowCommand : ICommand
    {
        private readonly IIoEngine engine;

        public ShowCommand(IIoEngine engine)
        {
            this.engine = engine;
        }

        public void Execute(string[] args, StudentsDatabase database)
        {
            string name = args[1];

            Student student = database.Find(name);

            if (student != null)
            {
                this.engine.Write(student.ToString());
            }
        }
    }
}
