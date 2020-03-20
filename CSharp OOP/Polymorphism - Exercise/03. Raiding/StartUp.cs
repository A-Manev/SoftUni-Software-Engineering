using Raiding.Core;
using Raiding.Core.Contracts;

namespace Raiding
{
    public class StartUp
    {
        public static void Main()
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
