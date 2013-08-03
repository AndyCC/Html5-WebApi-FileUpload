using System.Reflection;
using Jumbleblocks.Net.Core.Dynamic;

namespace Jumbleblocks.Net.Files
{
    public class FilePathMappingRule : Expando
    {
        public PropertyInfo PropertyToEvaluate { get; set; }
        public string SavePath { get; set; }
    }
}
