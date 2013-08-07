using Jumbleblocks.Net.Core.Configuration;
using Moq;

namespace Tests.Jumbleblocks.Net.Helpers
{
    public class WebConfigurationMock : Mock<IWebConfiguration>
    {
        public void SetUp_GetApplicationSetting_WithProvidedNameReturnsGivenValue(string name, string value)
        {
            Setup(x => x.GetApplicationSetting(name)).Returns(value);
        }

        public void Verify_GetApplicationSetting_CalledWithName(string expectedName, Times numberOfTimes)
        {
            Verify(x => x.GetApplicationSetting(expectedName), numberOfTimes);
        }
    }
}
