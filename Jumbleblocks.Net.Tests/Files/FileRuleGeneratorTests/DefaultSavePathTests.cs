using Jumbleblocks.Net.Files;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Files.FileRuleGeneratorTests
{
    [TestFixture]
    public class DefaultSavePathTests : TestBase<FileRuleGenerator<FakeFileOverHttp>>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new FileRuleGenerator<FakeFileOverHttp>();
        }

        private const string ValidFilePath = "~/App_Data/";

        [Test]
        public void ReturnsReferenceToContainingObject()
        {
            var physicalWithRule = ItemUnderTest.DefaultSavePath(ValidFilePath);
            physicalWithRule.Should().Equal(ItemUnderTest);
        }

        [Test]
        public void WithValidPath_ThenSetDefaultPathOnGeneratedRule()
        {
            ItemUnderTest.DefaultSavePath(ValidFilePath);
            var generatedRule = ItemUnderTest.GetFileMappingRuleSet();
            generatedRule.DefaultFilePath.Should().Equal(ValidFilePath);
        }

        [Test]
        public void WithInvalidPath_ThenThrowsInvalidFileMappingException()
        {
            const string invalidFileName = @"[[??@@:.,<>|\";

            var ex =  Assert.Throws<InvalidFilePathMappingException>(
                    () => ItemUnderTest.DefaultSavePath(invalidFileName)
                );

            ThenExceptionMessageShouldEqual(ex, string.Format("The file path '{0}' is invalid", invalidFileName));
        }

        [Test]
        public void WithDefaultSavePathAlreadySet_ThenThrowsFileMappingException()
        {
            ItemUnderTest.DefaultSavePath(ValidFilePath);

            var ex = Assert.Throws<FileMappingException>(
                    () => ItemUnderTest.DefaultSavePath(ValidFilePath)
                );

            ThenExceptionMessageShouldEqual(ex, "DefaultSavePath() can only be called once and it has already been called");
        }

    }
}
