using Jumbleblocks.Net.Files;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Files.FileRuleGeneratorTests
{
    [TestFixture]
    public class AddFilePathRuleTests : TestBase<FileRuleGenerator<FakeFileOverHttp>>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new FileRuleGenerator<FakeFileOverHttp>();
        }

        [Test]
        public void ReturnsReferenceToContainingObject()
        {
            var physicalWithRule = ItemUnderTest.AddFilePathRule;
            physicalWithRule.Should().Equal(ItemUnderTest);
        }

    }
}
