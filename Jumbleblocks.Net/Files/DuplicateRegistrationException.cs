using System;
using System.Runtime.Serialization;

namespace Jumbleblocks.Net.Files
{
    [Serializable]
    public class DuplicateRegistrationException : FileMappingException
    {
        public DuplicateRegistrationException()
        {
        }

        public DuplicateRegistrationException(string message)
            : base(message)
        {
        }

        public DuplicateRegistrationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DuplicateRegistrationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
