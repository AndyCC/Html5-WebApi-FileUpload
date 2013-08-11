using System.Net.Http;
using Jumbleblocks.Net.Formatting;
using Moq;

namespace Tests.Jumbleblocks.Net.Helpers
{
    public class MultipartFormDataStreamProviderFactoryMock : Mock<IMultipartFormDataStreamProviderFactory>
    {
        public MultipartFormDataStreamProviderFactoryMock()
        {
            Setup(x => x.CreateWithRootPath(It.IsAny<string>())).Returns(_multipartFormDataStreamProvider.Object);
        }

        private readonly Mock<MultipartFormDataStreamProvider> _multipartFormDataStreamProvider = new  Mock<MultipartFormDataStreamProvider>("~/App_Data/");

        public MultipartFormDataStreamProvider MultipartFormDataStreamProviderObject
        {
            get { return _multipartFormDataStreamProvider.Object; }
        }

        public void AddFormDataToBeReturnedByProvider(string key, string value)
        {
            _multipartFormDataStreamProvider.Object.FormData.Add(key, value);
        }

        public void Verify_CreatedMultipartFormDataStreamProviderCalledOnce_WithRoot(string expectedRoot)
        {
            Verify(x => x.CreateWithRootPath(It.Is<string>(p => p == expectedRoot)), Times.Once());       
        }
    }
}
