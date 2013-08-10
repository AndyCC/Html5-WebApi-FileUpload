using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Jumbleblocks.Net.Core.Configuration;
using Jumbleblocks.Net.Core.Reflection;
using Jumbleblocks.Net.Files;
using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Formatting
{
    public class FileMediaTypeFormatter : MediaTypeFormatter
    {
        public FileMediaTypeFormatter(IWebConfiguration webConfiguration, IMultipartFormDataStreamProviderFactory multipartFormDataStreamProviderFactory)
        {
            _webConfiguration = webConfiguration;
            _multipartFormDataStreamProviderFactory = multipartFormDataStreamProviderFactory;

            SetSupportedMediaTypes();
        }

        private readonly IWebConfiguration _webConfiguration;
        private readonly IMultipartFormDataStreamProviderFactory _multipartFormDataStreamProviderFactory;

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
                await content.ReadAsMultipartAsync(provider);

                //3: format form data into model
                var model = provider.ReadFormDataAs(type);


                //provider.FileData

                //formdata collection -> readas fdrom System.Web.Http.ModelBinding 

                return model;

            }
            catch (Exception ex)
            {
                formatterLogger.LogError(string.Empty, ex);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            //4: file mapping rules (outside of this) for use in controller
        }

        protected void AssertContentIsMimeMultipartContent(HttpContent content)
        {
            if (!content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
        }
        
        private MultipartFormDataStreamProvider CreateMultipartFormDataStreamProvider()
        {
            var temporyFileLocation = _webConfiguration.GetApplicationSetting("TemporaryFileUploadFolder");
            return _multipartFormDataStreamProviderFactory.CreateWithRootPath(temporyFileLocation);
        }


        //TODO: implement custom  MultipartFormDataStreamProvider : MultipartFileStreamProvider

        //TODO
          //catch (Exception e)
          //  {
          //      if (formatterLogger == null)
          //      {
          //          throw;
          //      }
          //      formatterLogger.LogError(String.Empty, e);
          //      return GetDefaultValueForType(type);
          //  }
    }
}
