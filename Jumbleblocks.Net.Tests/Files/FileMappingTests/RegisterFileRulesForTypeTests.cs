using System;
using System.Linq;
using Jumbleblocks.Net.Files;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Files.FileMappingTests
{
    [TestFixture]
    public class RegisterFileRulesForTypeTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            FileMapping.FilePathMappingRules.Clear();
        }

        [Test]
        public void AddsNewFileMappingRule_ToRulesForSpecifiedType_AndReturnsFileRuleGenerator()
        {
            var ruleGenerator = FileMapping.RegisterFileRulesForType<FakeFileOverHttp>();

            ThenObjectShouldBeOfType<FileRuleGenerator<FakeFileOverHttp>>(ruleGenerator);
            ThenEnumerationShouldCountExactly(FileMapping.FilePathMappingRules, 1);
            ThenRuleAtIndexShouldHaveFileModelTypeOf(0, typeof (FakeFileOverHttp));
        }
        
        [Test]
        public void WhenSameModelTypeRegisteredForSecondTime_ThenThrowsDuplicateRegistrationException()
        {
            FileMapping.RegisterFileRulesForType<FakeFileOverHttp>();

            var ex = Assert.Throws<DuplicateRegistrationException>(
                    () => FileMapping.RegisterFileRulesForType<FakeFileOverHttp>()
                );

            var expectedMessage = string.Format("The type '{0}' has already been registered", typeof(FakeFileOverHttp).FullName);
            ThenExceptionMessageShouldEqual(ex, expectedMessage);
        }

        private void ThenRuleAtIndexShouldHaveFileModelTypeOf(int index, Type expectedType)
        {
            FileMapping.FilePathMappingRules
                       .ElementAt(index)
                       .FileModelType
                       .Should()
                       .Equal(expectedType);
        }
    }
}
