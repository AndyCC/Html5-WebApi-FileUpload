using System.Net.Http;
using System.Threading.Tasks;

namespace Jumbleblocks.Net.Formatting
{
    public interface IHttpContentReader
    {
        Task<MultipartFormDataStreamProvider> ReadAsMultipartAsyncIntoProvider(HttpContent httpContent, MultipartFormDataStreamProvider streamProvider);
    }
}
