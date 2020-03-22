namespace Logger.Models.Contracts
{
    public interface IFile
    {
        //ILayout Layout { get; }

        string Path { get; }

        long Size { get; }

        string Write(ILayout layout, IError error);
    }
}
