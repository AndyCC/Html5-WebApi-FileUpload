using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using Jumbleblocks.Net.Files;
using Jumbleblocks.Net.Formatting;
using Moq;
using NUnit.Framework;
using Should.Fluent;
using Tests.Jumbleblocks.Net.Files;
using Tests.Jumbleblocks.Net.Helpers;

namespace Tests.Jumbleblocks.Net.Formatting.PhysicalFileMediaTypeFormatterTests
{
    [TestFixture]
    public class ReadFromStreamAsyncTests : TestBase<PhysicalFileMediaTypeFormatter>
    {
        private object _returnedObject;
        private Exception _exceptionThrown;

        private Type _type;
        private Mock<Stream> _stream;
        private Mock<IFormatterLogger> _formatterLogger;
        private HttpContentMock _httpContent;

        private FileMappingReaderMock _fileMappingReader;

        [SetUp]
        public void SetUp()
        {
            _fileMappingReader = new FileMappingReaderMock();
            ItemUnderTest = new PhysicalFileMediaTypeFormatter(_fileMappingReader.Object);

            _type = typeof(FakePhysicalFileOverHttp);
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
            ((HttpResponseException)_exceptionThrown).Response.StatusCode.Should().Equal(expectedStatusCode);
        }

        private void ThenShouldThrowFilePathMappingException()
        {
            _exceptionThrown.Should().Be.OfType<FileRulesException>();
        }

        [Test]
        public void WhenContentIsNotMimiMultipartContent_ThenThrowsHttpResponseExceptionWithUnsupportedMediaType()
        {
            _httpContent.Set_ContentTypeTo_TextHtml();

            Call_ReadFromStreamAsync();

            ThenHttpResponseExceptionThrownWithStatusCode(HttpStatusCode.UnsupportedMediaType);
        }

        //TODO: test when implements memory and physical file

        [Test]
        public void WithFileMappings_NotContainingType_WhenContentIsNotMimiMultipartContent_ThenThrowsFilePathMappingException()
        {
            _httpContent.Set_ContentTypeTo_MultipartFormDataWithBoundary();
            _fileMappingReader.Setup_PhysicalFilePathMappingRules_ToReturnEmptyList();

            Call_ReadFromStreamAsync();

            ThenShouldThrowFilePathMappingException();
        }

        
    }
}