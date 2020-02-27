using System;
using System.Collections.Generic;

namespace StudentSystem
{
    public class StudentSystem
    {
        private readonly IIoEngine ioEngine;

        private  StudentsDatabase students;

        private Dictionary<string, ICommand> commands;

        public StudentSystem(IIoEngine ioEngine)
        {
            this.students = new StudentsDatabase();
            this.commands = new Dictionary<string, ICommand>
            {
                { "Create", new CreateCommand() },
                { "Show", new ShowCommand(ioEngine) }
            };

            this.ioEngine = ioEngine;
        }

        public void ParseCommands()
        {
            while (true)
            {
                string[] args = this.ioEngine.Read().Split();

                string commandName = args[0];

                if (this.commands.ContainsKey(commandName))
                {
                    ICommand command = this.commands[commandName];
                    command.Execute(args, this.students);
                }
                else if (commandName == "Exit")
                {
                    return;
                }
            }
        }
    }
}
