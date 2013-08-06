using Jumbleblocks.Net.Files;
using Moq;

namespace Tests.Jumbleblocks.Net.Helpers
{
    public class FileMappingReaderMock : Mock<IFileMappingReader>
    {
        public void Setup_PhysicalFilePathMappingRules_ToReturnEmptyList()
        {
            SetupGet(x => x.PhysicalFilePathMappingRules).Returns(new FileMappingRuleSet[0]);
        }
    }
}
