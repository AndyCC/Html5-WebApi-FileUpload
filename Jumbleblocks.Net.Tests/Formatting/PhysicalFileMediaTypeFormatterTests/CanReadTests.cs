using Jumbleblocks.Net.Formatting;
using NUnit.Framework;
using Should.Fluent;
using Tests.Jumbleblocks.Net.Files;
using Tests.Jumbleblocks.Net.Helpers;

namespace Tests.Jumbleblocks.Net.Formatting.PhysicalFileMediaTypeFormatterTests
{
    public class CanReadTests : TestBase<PhysicalFileMediaTypeFormatter>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new PhysicalFileMediaTypeFormatter(new FileMappingReaderMock().Object);
        }

        [Test]
        public void WithTypeImplementationOfIMemoryFileOverHttp_ShouldReturnFalse()
        {
            ItemUnderTest.CanReadType(typeof(FakeMemoryFileModel)).Should().Be.False();
        }

        [Test]
        public void WithTypeImplementationOfIFileOverHttp_ShouldReturnTrue()
        {
            ItemUnderTest.CanReadType(typeof (FakePhysicalFileOverHttp)).Should().Be.True();
        }
    }
}
