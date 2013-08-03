using Jumbleblocks.Net.Models;

namespace Tests.Jumbleblocks.Net.Files
{
    public class FakeMemoryFileModel : IMemoryFileOverHttp
    {
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public byte[] Buffer { get; set; }
    }
}
