namespace StudentSystem
{
    public interface ICommand
    {
        void Execute(string[] args, StudentsDatabase database);
    }
}
