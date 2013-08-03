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

    public class FileMediaTypeFormatter : MediaTypeFormatter
    {
        public FileMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        }

        public override bool CanReadType(Type type)
        {
         //   return type == typeof(FileOverHttpBase) || type == typeof(MemoryFilePartOverHttp);
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public async override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            if (!content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            throw new NotImplementedException();
        }
    }
}

//MultipartMemoryStreamProvider 
//MultipartFormDataStreamProvider 