using System;
using Jumbleblocks.Net.Formatting;
using NUnit.Framework;
using Should.Fluent;
using Tests.Jumbleblocks.Net.Files;
using Tests.Jumbleblocks.Net.Helpers;

namespace Tests.Jumbleblocks.Net.Formatting.FileMediaTypeFormatterTests
{
    public class CanReadTests : TestBase<FileMediaTypeFormatter>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new FileMediaTypeFormatter(new WebConfigurationMock().Object, new MultipartFormDataStreamProviderFactoryMock().Object, new HttpContentReaderMock().Object);
        }

        [Test]
        public void WithTypeImplementationOfInt32_ShouldReturnFalse()
        {
            ItemUnderTest.CanReadType(typeof(Int32)).Should().Be.False();
        }

        [Test]
        public void WithTypeImplementationOfIFileOverHttp_ShouldReturnTrue()
        {
            ItemUnderTest.CanReadType(typeof (FakeFileOverHttp)).Should().Be.True();
        }
    }
}
