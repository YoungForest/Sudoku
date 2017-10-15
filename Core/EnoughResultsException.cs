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
    }
}