using System.Collections.Generic;
using System.Linq;

namespace Jumbleblocks.Net.Files
{
    public class ValidationResult
    {
        public ValidationResult(IEnumerable<string> failureMessages)
        {
            FailureMessages = failureMessages;
        }

        public bool IsValid { get { return FailureMessages.Count() == 0; } }
        public IEnumerable<string> FailureMessages { get; private set; }
    }
}
