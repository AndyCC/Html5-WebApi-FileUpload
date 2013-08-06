using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using Jumbleblocks.Net.Formatting;
using Moq;
using NUnit.Framework;
using Should.Fluent;
using Tests.Jumbleblocks.Net.Files;
using Tests.Jumbleblocks.Net.Helpers;

namespace Tests.Jumbleblocks.Net.Formatting.MemoryFileMediaTypeFormatterTests
{
    [TestFixture]
    public class ReadFromStreamAsyncTests : TestBase<MemoryFileMediaTypeFormatter>
    {
        private object _returnedObject;
        private Exception _exceptionThrown;

        private Type _type;
        private Mock<Stream> _stream;
        private Mock<IFormatterLogger> _formatterLogger;
        private HttpContentMock _httpContent;

        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new MemoryFileMediaTypeFormatter();

            _type = typeof (FakePhysicalFileOverHttp);
            _stream = new Mock<Stream>();
            _httpContent = new HttpContentMock();
            _formatterLogger = new Mock<IFormatterLogger>();

            _returnedObject = null;
        }

        public void Call_ReadFromStreamAsync()
        {
            try
            {
                var task = ItemUnderTest.ReadFromStreamAsync(_type, _stream.Object, _httpContent.Object,
                                                             _formatterLogger.Object);
                task.Wait();
            }
            catch (AggregateException ex)
            {
                _exceptionThrown = ex.InnerExceptions.First();
            }
        }

        private void ThenHttpResponseExceptionThrownWithStatusCode(HttpStatusCode expectedStatusCode)
        {
            _exceptionThrown.Should().Be.OfType<HttpResponseException>();
            ((HttpResponseException) _exceptionThrown).Response.StatusCode.Should().Equal(expectedStatusCode);
        }

        [Test]
        public void WhenContentIsNotMimiMultipartContent_ThenThrowsHttpResponseExceptionWithUnsupportedMediaType()
        {
            _httpContent.Set_ContentTypeTo_TextHtml();
            Call_ReadFromStreamAsync();
            ThenHttpResponseExceptionThrownWithStatusCode(HttpStatusCode.UnsupportedMediaType);
        }


    }
}