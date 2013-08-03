using System;
using System.Runtime.Serialization;

namespace Jumbleblocks.Net.Files
{
    [Serializable]
    public class InvalidFilePathMappingException : FileMappingException
    {
        public InvalidFilePathMappingException(string invalidFilePath)
            : base(FormatErrorMessage(invalidFilePath))
        {
        }
        
        public InvalidFilePathMappingException(string invalidFilePath, Exception inner)
            : base(FormatErrorMessage(invalidFilePath), inner)
        {
        }

        private static string FormatErrorMessage(string invalidFilePath)
        {
            return string.Format("The file path '{0}' is invalid", invalidFilePath);
        }

        protected InvalidFilePathMappingException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
