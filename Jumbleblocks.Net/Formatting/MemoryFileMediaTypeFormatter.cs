using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Jumbleblocks.Net.Core.Reflection;
using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Formatting
{
    public class MemoryFileMediaTypeFormatter : FileMediaTypeFormatter
    {
        public override bool CanReadType(Type type)
        {
            return type.IsImplementationOf(typeof (IMemoryFileOverHttp));
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public async override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            AssertContentIsMimeMultipartContent(content);

            throw new NotImplementedException();
        }
    }
}
