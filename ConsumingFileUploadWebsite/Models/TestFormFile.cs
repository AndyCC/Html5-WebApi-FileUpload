using System.Net.Http;
using Jumbleblocks.Net.Models;

namespace ConsumingFileUploadWebsite.Models
{
    public class TestFormFile : IFileOverHttp
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public MultipartFileData[] FileData { get; set; }
    }
}