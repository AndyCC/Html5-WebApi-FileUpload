using Jumbleblocks.Net.Formatting;
using Moq;

namespace Tests.Jumbleblocks.Net.Helpers
{
    public class MultipartFormDataStreamProviderFactoryMock : Mock<MultipartFormDataStreamProviderFactory>
    {
        public void Verify_CreatedMultipartFormDataStreamProviderCalledOnce_WithRoot(string expectedRoot)
        {
            Verify(x => x.CreateWithRootPath(It.Is<string>(p => p == expectedRoot)), Times.Once());       
        }
    }
}
