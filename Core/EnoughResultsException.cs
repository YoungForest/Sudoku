using System;
using System.Runtime.Serialization;

namespace Core
{
    [Serializable]
    internal class EnoughResultsException : Exception
    {
        public EnoughResultsException()
        {
        }

        public EnoughResultsException(string message) : base(message)
        {
        }

        public EnoughResultsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EnoughResultsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string ToString()
        {
            return "Enough sudoku get!";
        }
    }
}