using System.Net.Http;

namespace Jumbleblocks.Net.Models
{
    public interface IFileOverHttp 
    {
        MultipartFileData[] FileData { get; set; }
    }
}
