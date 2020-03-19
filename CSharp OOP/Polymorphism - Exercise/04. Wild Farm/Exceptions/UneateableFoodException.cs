using System;

namespace WildFarm.Exceptions
{
    public class UneateableFoodException : Exception
    {
        public UneateableFoodException(string message) 
            : base(message)
        {

        }
    }
}
