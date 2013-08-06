using System.Collections.Generic;

namespace Jumbleblocks.Net.Files
{
    public interface IFileMappingReader
    {
        IEnumerable<FileMappingRuleSet> PhysicalFilePathMappingRules { get; }
    }
}
