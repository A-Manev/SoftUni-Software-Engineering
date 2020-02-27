namespace StudentSystem
{
    public class StartUp
    {
        public static void Main()
        {
            StudentSystem studentSystem = new StudentSystem(new ConsoleIoEngine());

            studentSystem.ParseCommands();
        }
    }
}
