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

namespace Tests.Jumbleblocks.Net.Formatting.FileMediaTypeFormatterTests
{
    [TestFixture]
    public class ReadFromStreamAsyncTests : TestBase<FileMediaTypeFormatter>
    {
        private object _returnedObject;
        private Exception _exceptionThrown;

        private Type _type;
        private Mock<Stream> _stream;
        private Mock<IFormatterLogger> _formatterLogger;
        private HttpContentMock _httpContent;
        private HttpContentReaderMock _httpContentReader;

        private WebConfigurationMock _webConfiguration;
        private MultipartFormDataStreamProviderFactoryMock _multipartFormDataStreamProvider;

        [SetUp]
        public void SetUp()
        {
            _webConfiguration = new WebConfigurationMock();
            _multipartFormDataStreamProvider = new MultipartFormDataStreamProviderFactoryMock();
            _httpContentReader = new HttpContentReaderMock();

            ItemUnderTest = new FileMediaTypeFormatter(_webConfiguration.Object, 
                                                       _multipartFormDataStreamProvider.Object,
                                                       _httpContentReader.Object);

            _type = typeof(FakeFileOverHttp);
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

                _returnedObject = task.Result;
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

        [Test]
        public void WhenContentIsNotMimiMultipartContent_ThenThrowsHttpResponseExceptionWithUnsupportedMediaType()
        {
            _httpContent.Set_ContentTypeTo_TextHtml();

            Call_ReadFromStreamAsync();

            ThenHttpResponseExceptionThrownWithStatusCode(HttpStatusCode.UnsupportedMediaType);
        }

        [Test]
        public void FetchesTemporaryFileLocation_FromConfiguration_AndCreatesMultiPartFormDataStreamProviderWithIt()
        {
            const string webConfigKey = "TemporaryFileUploadFolder";
            const string expectedFolderLocation = "~/App_Data/";

            _httpContent.Set_ContentDispositionTo_FormData();
            _httpContent.Set_ContentTypeTo_MultipartFormDataWithBoundary();

            _webConfiguration.SetUp_GetApplicationSetting_WithProvidedNameReturnsGivenValue(webConfigKey, expectedFolderLocation);

            Call_ReadFromStreamAsync();

            _webConfiguration.Verify_GetApplicationSetting_CalledWithName(webConfigKey, Times.Once());
            _multipartFormDataStreamProvider.Verify_CreatedMultipartFormDataStreamProviderCalledOnce_WithRoot(expectedFolderLocation);
        }

        [Test]
        public void PassesHttpContent_AndProvider_ToHttpContentReader_ReadAsMultipartAsyncIntoProvider()
        {
            const string webConfigKey = "TemporaryFileUploadFolder";
            const string expectedFolderLocation = "~/App_Data/";

            _httpContent.Set_ContentDispositionTo_FormData();
            _httpContent.Set_ContentTypeTo_MultipartFormDataWithBoundary();

            _webConfiguration.SetUp_GetApplicationSetting_WithProvidedNameReturnsGivenValue(webConfigKey, expectedFolderLocation);

            Call_ReadFromStreamAsync();

            _httpContentReader.Verify_ReadAsMultipartAsyncIntoProvider_CalledOnceWith(_httpContent.Object, _multipartFormDataStreamProvider.MultipartFormDataStreamProviderObject);
        }

        [Test]
        public void ReturnsModel_PopulatedWithFormData()
        {
            const string expectedValue = "myproperty";

            _type = typeof(FakeFileOverHttp2);
            _httpContent.Set_ContentDispositionTo_FormData();
            _httpContent.Set_ContentTypeTo_MultipartFormDataWithBoundary();

            _webConfiguration.SetUp_GetApplicationSetting_WithProvidedNameReturnsGivenValue("TemporaryFileUploadFolder", "~/App_Data/");
            _multipartFormDataStreamProvider.AddFormDataToBeReturnedByProvider("PropertySetByModelBinding", expectedValue);

            _httpContentReader.Setup_ReadAsMultipartAsyncIntoProvider_ToReturn(_multipartFormDataStreamProvider.MultipartFormDataStreamProviderObject);

            Call_ReadFromStreamAsync();

            ThenObjectShouldBeOfType(_returnedObject, _type);
            ThenPropertyShouldEqual((FakeFileOverHttp2)_returnedObject, x=> x.PropertySetByModelBinding, expectedValue);

        }

        //TODO: mock MultipartFormDataStreamProvider and test returns correct filename to mapped model

        //TODO: test mapping form data into an object 
        //need serialiser (1)properties, subproperties, arrays (plus casting to differnt types) + look at json to see how it handles errors
        //there must be something to do this, as MVC model binding has to do it

        
    }
}