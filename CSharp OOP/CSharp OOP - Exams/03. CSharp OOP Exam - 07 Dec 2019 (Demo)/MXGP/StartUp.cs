namespace MXGP
{
    using MXGP.Core;
    using MXGP.Core.Contracts;

    public class StartUp
    {
        public static void Main()
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
