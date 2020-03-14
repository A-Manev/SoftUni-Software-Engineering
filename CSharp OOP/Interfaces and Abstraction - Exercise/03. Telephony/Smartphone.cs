using System.Linq;

namespace Telephony
{
    public class Smartphone : ICallable, IBrowseable
    {
        public string Call(string number)
        {
            if (!number.All(char.IsDigit))
            {
                return "Invalid number!";
            }

            return $"Calling... {number}";
        }

        public string Browse(string site)
        {
            if (site.Any(char.IsDigit))
            {
                return "Invalid URL!";
            }

            return $"Browsing: {site}!";
        }
    }
}
