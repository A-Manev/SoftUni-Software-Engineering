namespace StudentSystem
{
    public interface IIoEngine
    {
        string Read();

        void Write(string str);
    }
}