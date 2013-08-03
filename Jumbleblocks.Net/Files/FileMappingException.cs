using System;
using System.Runtime.Serialization;

namespace Jumbleblocks.Net.Files
{
    [Serializable]
    public class FileMappingException : Exception
    {
        public FileMappingException()
        {
        }

        public FileMappingException(string message) : base(message)
        {
        }

        public FileMappingException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FileMappingException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
