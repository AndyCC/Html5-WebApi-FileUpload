using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Jumbleblocks.Net.Formatting
{
    //TODO: need impl for physical and memory

    public abstract class FileMediaTypeFormatter : MediaTypeFormatter
    {
        protected FileMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        }

        protected void AssertContentIsMimeMultipartContent(HttpContent content)
        {
            if (!content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
        }
    }
}

//MultipartMemoryStreamProvider 
//MultipartFormDataStreamProvider 