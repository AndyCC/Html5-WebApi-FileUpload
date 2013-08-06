using System.Net.Http.Formatting;
using Moq;

namespace Tests.Jumbleblocks.Net.Helpers
{
    public class MockFormatterLogger : Mock<IFormatterLogger>
    {
        public void Verify_LogErrorCalledWithMessageOnce(string expectedErrorPath, string expectedMessage)
        {
            Verify(x => x.LogError(expectedErrorPath, expectedMessage), Times.Once());
        }
    }
}
