using Jumbleblocks.Net.Core.Configuration;
using Jumbleblocks.Net.Formatting;
using NUnit.Framework;
using Should.Fluent;
using Tests.Jumbleblocks.Net.Helpers;

namespace Tests.Jumbleblocks.Net.Formatting.FileMediaTypeFormatterTests
{
    [TestFixture]
    public class SupportedMediaTypesTests : TestBase<FileMediaTypeFormatter>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new FileMediaTypeFormatter(new WebConfigurationMock().Object, new MultipartFormDataStreamProviderFactoryMock().Object, new HttpContentReaderMock().Object);
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
