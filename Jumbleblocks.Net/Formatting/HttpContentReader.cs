using System.Net.Http;
using System.Threading.Tasks;

namespace Jumbleblocks.Net.Formatting
{
    public class HttpContentReader : IHttpContentReader
    {
        public Task<MultipartFormDataStreamProvider> ReadAsMultipartAsyncIntoProvider(HttpContent httpContent, MultipartFormDataStreamProvider streamProvider)
        {
            return httpContent.ReadAsMultipartAsync(streamProvider);
        }
    }
}
