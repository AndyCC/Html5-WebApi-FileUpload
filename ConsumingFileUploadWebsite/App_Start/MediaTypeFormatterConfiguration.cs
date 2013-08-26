using System.Net.Http.Formatting;
using Jumbleblocks.Net.Core.Configuration;
using Jumbleblocks.Net.Formatting;

namespace ConsumingFileUploadWebsite.App_Start
{
    public static class MediaTypeFormatterConfiguration
    {
        public static void RegisterMediaTypeFormatters(MediaTypeFormatterCollection collection)
        {
            //TODO: use DI to inject dependencies
            var webConfiguration = new WebConfigurationManagerWrapper();
            var formDataStreamProviderFactory = new MultipartFormDataStreamProviderFactory();
            var httpContentReader = new HttpContentReader();

            collection.Add(new FileMediaTypeFormatter(webConfiguration, formDataStreamProviderFactory, httpContentReader));

        }
    }
}