using Jumbleblocks.Net.Models;

namespace Tests.Jumbleblocks.Net.Files
{
    public class FakePhysicalFileOverHttp : IPhysicalFileOverHttp
    {
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public string FullFilePath { get; set; }

        public string TestVairable = "Vairable";

        public string TestMethod()
        {
            return "hi";
        }
    }
}
