using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Formatting.FileMediaTypeFormatterTests
{
    [TestFixture]
    public class SupportedMediaTypesTests : FileMediaTypeFormatterTestBase
    {
        [Test]
        public void Supports_OctetStream_MediaType()
        {
            ItemUnderTest.SupportedMediaTypes.Should().Contain.One(mdh => mdh.MediaType == "application/octet-stream");
        }

        [Test]
        public void Supports_FormData_MediaType()
        {
            ItemUnderTest.SupportedMediaTypes.Should().Contain.One(mdh => mdh.MediaType == "multipart/form-data");
        }
    }
}
