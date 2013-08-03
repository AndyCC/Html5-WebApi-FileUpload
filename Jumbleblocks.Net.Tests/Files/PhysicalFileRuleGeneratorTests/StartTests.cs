using Jumbleblocks.Net.Files;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Files.PhysicalFileRuleGeneratorTests
{
    [TestFixture]
    public class StartTests : TestBase<PhysicalFileRuleGenerator<FakePhysicalFileOverHttp>>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new PhysicalFileRuleGenerator<FakePhysicalFileOverHttp>();
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
            rule.FileModelType.Should().Equal(typeof (FakePhysicalFileOverHttp));
        }
    }
}
