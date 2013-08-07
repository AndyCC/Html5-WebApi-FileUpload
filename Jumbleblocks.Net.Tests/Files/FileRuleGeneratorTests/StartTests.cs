using Jumbleblocks.Net.Files;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Files.FileRuleGeneratorTests
{
    [TestFixture]
    public class StartTests : TestBase<FileRuleGenerator<FakeFileOverHttp>>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new FileRuleGenerator<FakeFileOverHttp>();
        }

        [Test]
        public void ReturnsReferenceToContainingObject()
        {
            var physicalWithRule = ItemUnderTest.Start();
            physicalWithRule.Should().Equal(ItemUnderTest);
        }

        [Test]
        public void WithTModelType_ThenSetsGeneratedRulesFileModelTypeToTModelType()
        {
            ItemUnderTest.Start();
            var rule = ItemUnderTest.GetFileMappingRuleSet();
            rule.FileModelType.Should().Equal(typeof (FakeFileOverHttp));
        }
    }
}
