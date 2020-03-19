using WildFarm.Core;
using WildFarm.Core.Contracts;

namespace WildFarm
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
