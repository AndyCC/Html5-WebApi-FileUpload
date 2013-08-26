using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Jumbleblocks.Net.Core.Configuration;
using Jumbleblocks.Net.Core.Reflection;
using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Formatting
{
    public class FileMediaTypeFormatter : MediaTypeFormatter
    {
        public FileMediaTypeFormatter(IWebConfiguration webConfiguration, 
                                      IMultipartFormDataStreamProviderFactory multipartFormDataStreamProviderFactory,
                                      IHttpContentReader httpContentReader)
        {
            _webConfiguration = webConfiguration;
            _multipartFormDataStreamProviderFactory = multipartFormDataStreamProviderFactory;
            _httpContentReader = httpContentReader;

            SetSupportedMediaTypes();
        }

        private readonly IWebConfiguration _webConfiguration;
        private readonly IMultipartFormDataStreamProviderFactory _multipartFormDataStreamProviderFactory;
        private readonly IHttpContentReader _httpContentReader;

        protected FileMediaTypeFormatter()
        {
        }

        private void SetSupportedMediaTypes()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        }

        public override bool CanReadType(Type type)
        {
            return type.IsImplementationOf(typeof(IFileOverHttp));
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public async override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            AssertContentIsMimeMultipartContent(content);
            var provider = CreateMultipartFormDataStreamProvider();

            try
            {
                await _httpContentReader.ReadAsMultipartAsyncIntoProvider(content, provider);
                var model = provider.ReadFormDataAs(type);
                ((IFileOverHttp) model).FileData = provider.FileData.ToArray();
                
                return model;
            }
            catch (Exception ex)
            {
                formatterLogger.LogError(string.Empty, ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        protected void AssertContentIsMimeMultipartContent(HttpContent content)
        {
            if (!content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        }
        
        private MultipartFormDataStreamProvider CreateMultipartFormDataStreamProvider()
        {
            var temporyFileLocation = _webConfiguration.GetApplicationSetting("TemporaryFileUploadFolder");
            var locationOnServer = HttpContext.Current.Server.MapPath(temporyFileLocation) ?? temporyFileLocation;
            return _multipartFormDataStreamProviderFactory.CreateWithRootPath(locationOnServer);
        }
    }
}
