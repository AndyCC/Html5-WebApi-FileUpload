using Jumbleblocks.Net.Models;

namespace Tests.Jumbleblocks.Net.Files
{
    public class FakeFileOverHttp : IFileOverHttp
    {
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public string[] FilePaths { get; set; }      
        public string TestVairable = "Vairable";

        public string TestMethod()
        {
            return "hi";
        }


    }

    public class FakeFileOverHttp2 : IFileOverHttp
    {
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public string[] FilePaths { get; set; }

        public string PropertySetByModelBinding { get; set; }
    }
}
