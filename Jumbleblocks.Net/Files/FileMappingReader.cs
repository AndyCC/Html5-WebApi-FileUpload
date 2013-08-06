using System.Collections.Generic;

namespace Jumbleblocks.Net.Files
{
    public class FileMappingReader : IFileMappingReader
    {
        public IEnumerable<FileMappingRuleSet> PhysicalFilePathMappingRules
        {
            get { return FileMapping.PhysicalFilePathMappingRules; }
        }
    }
}
