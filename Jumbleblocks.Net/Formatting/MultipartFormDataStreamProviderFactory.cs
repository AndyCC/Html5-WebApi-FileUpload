using System.Net.Http;

namespace Jumbleblocks.Net.Formatting
{
    public class MultipartFormDataStreamProviderFactory : IMultipartFormDataStreamProviderFactory
    {
        public MultipartFormDataStreamProvider CreateWithRootPath(string root)
        {
            return  new MultipartFormDataStreamProvider(root);
        }
    }
}
