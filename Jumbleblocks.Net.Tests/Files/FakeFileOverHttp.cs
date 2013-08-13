using System.Net.Http;
using Jumbleblocks.Net.Models;

namespace Tests.Jumbleblocks.Net.Files
{
    public class FakeFileOverHttp : IFileOverHttp
    {    
        public string TestVairable = "Vairable";

        public string TestMethod()
        {
            return "hi";
        }
        
        public MultipartFileData[] FileData { get; set; }

        //TODO: refactor these out
        public string FileName { get; set; }
    }

    public class FakeFileOverHttp2 : IFileOverHttp
    {
        public string PropertySetByModelBinding { get; set; }
        public MultipartFileData[] FileData { get; set; }
    }
}
