using Jumbleblocks.Net.Formatting;
using Jumbleblocks.Net.Models;
using NUnit.Framework;
using Should.Fluent;
using Tests.Jumbleblocks.Net.Files;

namespace Tests.Jumbleblocks.Net.Formatting.MemoryFileMediaTypeFormatterTests
{
    public class CanReadTests : TestBase<MemoryFileMediaTypeFormatter>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new MemoryFileMediaTypeFormatter();
        }

        [Test]
        public void WithTypeImplementationOfIMemoryFileOverHttp_ShouldReturnTrue()
        {
            ItemUnderTest.CanReadType(typeof(FakeMemoryFileModel)).Should().Be.True();
        }

        [Test]
        public void WithTypeImplementationOfIFileOverHttp_ShouldReturnFalse()
        {
            ItemUnderTest.CanReadType(typeof (FakePhysicalFileOverHttp)).Should().Be.False();
        }
    }
}
