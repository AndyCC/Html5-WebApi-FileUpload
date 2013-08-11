using System.Net.Http;
using System.Threading.Tasks;
using Jumbleblocks.Net.Formatting;
using Moq;

namespace Tests.Jumbleblocks.Net.Helpers
{
    public class HttpContentReaderMock : Mock<IHttpContentReader>
    {

        public void Setup_ReadAsMultipartAsyncIntoProvider_ToReturn(MultipartFormDataStreamProvider provider)
        {
            Setup(x => x.ReadAsMultipartAsyncIntoProvider(It.IsAny<HttpContent>(), It.IsAny<MultipartFormDataStreamProvider>()))
              .Returns(() =>
                  {
                      return Task<MultipartFormDataStreamProvider>.Factory.StartNew(() => provider);
                  }
                )
              .Verifiable();
        }

        public void Verify_ReadAsMultipartAsyncIntoProvider_CalledOnceWith(HttpContent expectedHttpContent,
                                                                           MultipartFormDataStreamProvider expectedProvider)
        {
            Verify(x => x.ReadAsMultipartAsyncIntoProvider(It.Is<HttpContent>(c => c == expectedHttpContent), 
                                                           It.Is<MultipartFormDataStreamProvider>(p => p == expectedProvider)),
                                                           Times.Once());
        }

    }
}
