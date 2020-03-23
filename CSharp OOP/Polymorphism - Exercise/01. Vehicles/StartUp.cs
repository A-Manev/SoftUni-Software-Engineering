using Vehicles.Core;
using Vehicles.Core.Contracts;

namespace Vehicles
{
    public class Program
    {
        public static void Main()
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
