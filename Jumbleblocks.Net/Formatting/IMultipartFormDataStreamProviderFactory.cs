using System.Net.Http;

namespace Jumbleblocks.Net.Formatting
{
    public interface IMultipartFormDataStreamProviderFactory
    {
        MultipartFormDataStreamProvider CreateWithRootPath(string root);
    }
}
