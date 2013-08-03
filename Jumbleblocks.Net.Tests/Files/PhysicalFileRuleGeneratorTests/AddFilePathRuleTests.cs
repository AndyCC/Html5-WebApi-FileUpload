using Jumbleblocks.Net.Files;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Files.PhysicalFileRuleGeneratorTests
{
    [TestFixture]
    public class AddFilePathRuleTests : TestBase<PhysicalFileRuleGenerator<FakePhysicalFileOverHttp>>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new PhysicalFileRuleGenerator<FakePhysicalFileOverHttp>();
        }

        [Test]
        public void ReturnsReferenceToContainingObject()
        {
            var physicalWithRule = ItemUnderTest.AddFilePathRule;
            physicalWithRule.Should().Equal(ItemUnderTest);
        }

    }
}
