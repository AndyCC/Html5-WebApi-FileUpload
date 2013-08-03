using System;
using System.Runtime.Serialization;

namespace Jumbleblocks.Net.Core.Expressions
{
    [Serializable]
    public class MemberExpressionException : ExpressionException
    {
        public MemberExpressionException(string memberName,  Type classType, string message)
            : base(message)
        {
            MemberName = memberName;
            ClassType = classType;
        }

        public MemberExpressionException(string memberName,  Type classType, string message, Exception inner)
            : base(message, inner)
        {
            MemberName = memberName;
            ClassType = classType;
        }

        protected MemberExpressionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public string MemberName { get; private set; }
        public Type ClassType { get; private set; }

    }
}
