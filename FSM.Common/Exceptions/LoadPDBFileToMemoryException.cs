using System;

namespace FSM.Common.Exceptions
{
    public class LoadPDBFileToMemoryException : Exception
    {
        public string Line { get; private set; }

        public LoadPDBFileToMemoryException(string line, string message, Exception innerException) : base(message, innerException)
        {
            Line = line;
        }
    }
}
