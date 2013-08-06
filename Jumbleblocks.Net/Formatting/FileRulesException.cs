using System;
using System.Runtime.Serialization;

namespace Jumbleblocks.Net.Formatting
{
    [Serializable]
    public class FileRulesException : Exception
    {
        public FileRulesException()
        {
        }

        public FileRulesException(string message) : base(message)
        {
        }

        public FileRulesException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FileRulesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
