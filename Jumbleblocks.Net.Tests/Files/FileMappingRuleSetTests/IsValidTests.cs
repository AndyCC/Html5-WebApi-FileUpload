using System;
using Jumbleblocks.Net.Files;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Files.FileMappingRuleSetTests
{
    [TestFixture]
    public class IsValidTests : TestBase<FileMappingRuleSet>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new FileMappingRuleSet();
        }

        private const string ValidFilePath = "~/App_Data/";
        private ValidationResult _validationResult;

        [Test]
        public void WhenFileModelTypeIsNull_ThenReturnsFalse()
        {
            GivenItemUnderTestSetUpWith(ValidFilePath);
            WhenIsValidCalled();
            ThenValidationResultIsFalse();
            ThenValidationResultContainsMessage("FileModelType is null");
        }

        [Test]
        public void WhenDefaultFilePathIsNull_ThenReturnsFalse()
        {
            GivenItemUnderTestSetUpWith(fileModelType: typeof(FakePhysicalFileOverHttp));
            WhenIsValidCalled();
            ThenValidationResultIsFalse();
            ThenValidationResultContainsMessage("DefaultFilePath is null or empty");
        }

        [Test]
        public void WhenDefaultFilePathIsEmptyString_ThenReturnsFalse()
        {
            GivenItemUnderTestSetUpWith(string.Empty, typeof(FakePhysicalFileOverHttp));
            WhenIsValidCalled();
            ThenValidationResultIsFalse();
            ThenValidationResultContainsMessage("DefaultFilePath is null or empty");
        }

        [Test]
        public void WhenDefaultFilePath_AndFileModelType_HaveValues_ThenReturnsTrue()
        {
            GivenItemUnderTestSetUpWith(ValidFilePath, typeof(FakePhysicalFileOverHttp));
            WhenIsValidCalled();
            ThenValidationResultIsTrue();
            ThenValidationResultContainsNoMessages();
        }

        public void GivenItemUnderTestSetUpWith(string defaultFilePath = null, Type fileModelType = null)
        {
            ItemUnderTest.DefaultFilePath = defaultFilePath;
            ItemUnderTest.FileModelType = fileModelType;
        }

        private void WhenIsValidCalled()
        {
            _validationResult = ItemUnderTest.IsValid();
        }

        private void ThenValidationResultIsFalse()
        {
            _validationResult.IsValid.Should().Be.False();
        }

        private void ThenValidationResultIsTrue()
        {
            _validationResult.IsValid.Should().Be.True();
        }

        private void ThenValidationResultContainsMessage(string expectedMessage)
        {
            _validationResult.FailureMessages.Should().Contain.One(expectedMessage);
        }

        private void ThenValidationResultContainsNoMessages()
        {
            ThenEnumerationShouldCountExactly(_validationResult.FailureMessages, 0);
        }

    }
}
