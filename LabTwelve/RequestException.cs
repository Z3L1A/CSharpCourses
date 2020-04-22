using System;

namespace LabTwelve
{
    public class RequestException : Exception
    {
        public RequestException()
        {
            
        }

        public RequestException(string input, Exception e = null) : base(string.Format($"Error on processing request with input: '{input}'"), e)
        {
            
        }
    }
}