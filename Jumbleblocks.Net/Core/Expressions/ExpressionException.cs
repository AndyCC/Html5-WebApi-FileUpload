using System;
using System.Runtime.Serialization;

namespace Jumbleblocks.Net.Core.Expressions
{
    [Serializable]
    public class ExpressionException : Exception
    {
        public ExpressionException()
        {
            
        }

        public ExpressionException(string message) : base(message)
        {
        }

        public ExpressionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ExpressionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
