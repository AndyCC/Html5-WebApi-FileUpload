using System;
using System.Linq.Expressions;
using Jumbleblocks.Net.Core.Expressions;
using Jumbleblocks.Net.Files;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Files.PhysicalFileRuleGeneratorTests
{
    [TestFixture]
    public class WhenPropertyMatchesSaveToTests : TestBase<PhysicalFileRuleGenerator<FakePhysicalFileOverHttp>>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new PhysicalFileRuleGenerator<FakePhysicalFileOverHttp>();
        }

        protected FileMappingRuleSet GeneratedFileMappingRuleSet
        {
            get { return ItemUnderTest.GetFileMappingRuleSet(); }
        }

        const string ValidFilePath = "~/App_Data/";
        private IPhysicalWithRule<FakePhysicalFileOverHttp> _returnedObject;
            
        [Test]
        public void ReturnsReferenceToContainingObject()
        {
            WhenFileNamePropertyMatchesTestThenSaveTo(ValidFilePath);
            _returnedObject.Should().Equal(ItemUnderTest);
        }
        
        [Test]
        public void WhenSaveToPathIsValid_ThenAddsFilePathMappingRuleToPhysicalFileMappingRule()
        {
            WhenFileNamePropertyMatchesTestThenSaveTo(ValidFilePath);
            ThenGeneratedRuleShouldHaveFilePathMappingRulesCountOf(1);
            ThenGeneratedRuleReferencesPropertyFileName(0);
            ThenGeneratedRuleSaveToPathIs(0,ValidFilePath);
            ThenGeneratedRuleEvaluatorReturnsTrueWhenPassedTest(0);
            ThenGeneratedRuleEvaluatorReturnsFalseWhenPassedTooo(0);
        }

        [Test]
        public void WhenSaveToFilePathIsInvalid_ThenDoesNotAddMappingRule_AndThrowsInvalidFileMappingException()
        {
            const string invalidFileName = @"[[??@@:.,<>|\";

           var ex = Assert.Throws<InvalidFilePathMappingException>(
                    () => WhenFileNamePropertyMatchesTestThenSaveTo(invalidFileName)
                );

            ThenExceptionMessageShouldEqual(ex, string.Format("The file path '{0}' is invalid", invalidFileName));

            ThenGeneratedRuleShouldHaveFilePathMappingRulesCountOf(0);
        }

        [Test]
        public void WhenPropertyIsAVairable_ThenDoesNotAddMappingRule_AndThrowsInvalidFileMappingException()
        {
            var vairableName = ExpressionHelper.GetMemberName<FakePhysicalFileOverHttp, string>(x => x.TestVairable);

            var ex = Assert.Throws<FileMappingException>(
                  () => WhenPropertyIsDefinedByExpression(x => x.TestVairable)
                );

            ThenExceptionMessageShouldEqual(ex, string.Format("Can only map to properties. '{0}' of type '{1}' is not a property.", vairableName, typeof(FakePhysicalFileOverHttp).FullName));
            ThenGeneratedRuleShouldHaveFilePathMappingRulesCountOf(0);
        }

        [Test]
        public void WhenPropertyIsAMethod_ThenDoesNotAddMappingRule_AndThrowsInvalidFileMappingException()
        {
            var methodName = ExpressionHelper.GetMethodName<FakePhysicalFileOverHttp, string>(x => x.TestMethod());

            var ex = Assert.Throws<FileMappingException>(
                  () => WhenPropertyIsDefinedByExpression(x => x.TestMethod())
                );


            ThenExceptionMessageShouldEqual(ex, string.Format("Can only map to properties. '{0}' of type '{1}' is not a property.", methodName, typeof(FakePhysicalFileOverHttp).FullName));
            ThenGeneratedRuleShouldHaveFilePathMappingRulesCountOf(0);
        }

        private void WhenFileNamePropertyMatchesTestThenSaveTo(string saveTo)
        {
            _returnedObject = ItemUnderTest.WhenPropertyMatchesSaveTo(x => x.FileName, x => x == "Test", saveTo);
        }

        private void WhenPropertyIsDefinedByExpression(Expression<Func<FakePhysicalFileOverHttp, string>> method)
        {
            ItemUnderTest.WhenPropertyMatchesSaveTo(method, x => x == "Test", ValidFilePath);
        }

        private void ThenGeneratedRuleShouldHaveFilePathMappingRulesCountOf(int expectedRuleCount)
        {
            ThenEnumerationShouldCountExactly(GeneratedFileMappingRuleSet.FilePathMappingRules, expectedRuleCount);
        }

        private void ThenGeneratedRuleReferencesPropertyFileName(int ruleIndex)
        {
            GeneratedFileMappingRuleSet.FilePathMappingRules[ruleIndex].PropertyToEvaluate.Name
                         .Should().Equal("FileName");
        }

        private void ThenGeneratedRuleSaveToPathIs(int ruleIndex, string expectedPath)
        {
            GeneratedFileMappingRuleSet.FilePathMappingRules[ruleIndex].SavePath.Should().Equal(expectedPath);
        }

        private void ThenGeneratedRuleEvaluatorReturnsTrueWhenPassedTest(int ruleIndex)
        {
            dynamic rule = GeneratedFileMappingRuleSet.FilePathMappingRules[ruleIndex];
            bool result = rule.Evaluate("Test");

            result.Should().Be.True();
        }

        private void ThenGeneratedRuleEvaluatorReturnsFalseWhenPassedTooo(int ruleIndex)
        {
            dynamic rule = GeneratedFileMappingRuleSet.FilePathMappingRules[ruleIndex];
            bool result = rule.Evaluate("Tooo");

            result.Should().Be.False();
        }
    }
}
