using System.Linq;

namespace Telephony
{
    public class StationaryPhone : ICallable
    {
        public string Call(string number)
        {
            if (!number.All(char.IsDigit))
            {
                return "Invalid number!";
            }

            return $"Dialing... {number}";
        }
    }
}
