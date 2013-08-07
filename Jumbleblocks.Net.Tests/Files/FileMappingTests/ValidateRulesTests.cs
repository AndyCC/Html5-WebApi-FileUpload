using Jumbleblocks.Net.Files;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Files.FileMappingTests
{
    [TestFixture]
    public class ValidateRulesTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            FileMapping.FilePathMappingRules.Clear();
            _validationResultCollection = null;
        }

        private ValidationResultCollection _validationResultCollection;

        [Test]
        public void WhenNoValidationErrors_ThenReturnsValidatedAsTrue()
        {
            FileMapping.RegisterFileRulesForType<FakeFileOverHttp>()
                       .DefaultSavePath("~/App_Data/");

            WhenValidatingRules();
            ThenValidationResultIsTrue();
            ThenValidationRuleCountsShouldBe(1, 0);
        }

        [Test]
        public void WithOneInvalidMapping_ThenReturnsValidatedAsFalse()
        {
            FileMapping.RegisterFileRulesForType<FakeFileOverHttp>();

            WhenValidatingRules();
            ThenValidationResultIsFalse();
            ThenValidationRuleCountsShouldBe(0, 1);
        }

        [Test]
        public void WithOneValidAndOneInvalidMapping_ThenReturnsValidatedAsFalse()
        {
            FileMapping.RegisterFileRulesForType<FakeFileOverHttp>()
                     .DefaultSavePath("~/App_Data/");

            FileMapping.RegisterFileRulesForType<FakeFileOverHttp2>();

            WhenValidatingRules();
            ThenValidationResultIsFalse();
            ThenValidationRuleCountsShouldBe(1, 1);
        }

        private void WhenValidatingRules()
        {
            _validationResultCollection = FileMapping.ValidateRules();
        }

        private void ThenValidationResultIsTrue()
        {
            _validationResultCollection.AllRulesAreValid.Should().Be.True();
        }

        private void ThenValidationResultIsFalse()
        {
            _validationResultCollection.AllRulesAreValid.Should().Be.False();
        }

        private void ThenValidationRuleCountsShouldBe(int expectedNumValidResults, int expectedNumInvalidResults)
        {
            ThenEnumerationShouldCountExactly(_validationResultCollection.ValidResults, expectedNumValidResults);
            ThenEnumerationShouldCountExactly(_validationResultCollection.InvalidResults, expectedNumInvalidResults);
        }
    }
}
