using Jumbleblocks.Net.Formatting;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Formatting.MemoryFileMediaTypeFormatterTests
{
    [TestFixture]
    public class SupportedMediaTypesTests : TestBase<MemoryFileMediaTypeFormatter>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new MemoryFileMediaTypeFormatter();
        }

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
