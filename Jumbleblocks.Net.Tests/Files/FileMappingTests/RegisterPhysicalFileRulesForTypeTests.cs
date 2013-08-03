using System;
using System.Linq;
using Jumbleblocks.Net.Files;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Files.FileMappingTests
{
    [TestFixture]
    public class RegisterPhysicalFileRulesForTypeTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            FileMapping.PhysicalFilePathMappingRules.Clear();
        }

        [Test]
        public void AddsNewFileMappingRule_ToRulesForSpecifiedType_AndReturnsPhysicalFileRuleGenerator()
        {
            var ruleGenerator = FileMapping.RegisterPhysicalFileRulesForType<FakePhysicalFileOverHttp>();

            ThenObjectShouldBeOfType<PhysicalFileRuleGenerator<FakePhysicalFileOverHttp>>(ruleGenerator);
            ThenEnumerationShouldCountExactly(FileMapping.PhysicalFilePathMappingRules, 1);
            ThenRuleAtIndexShouldHaveFileModelTypeOf(0, typeof (FakePhysicalFileOverHttp));
        }
        
        [Test]
        public void WhenSameModelTypeRegisteredForSecondTime_ThenThrowsDuplicateRegistrationException()
        {
            FileMapping.RegisterPhysicalFileRulesForType<FakePhysicalFileOverHttp>();

            var ex = Assert.Throws<DuplicateRegistrationException>(
                    () => FileMapping.RegisterPhysicalFileRulesForType<FakePhysicalFileOverHttp>()
                );

            var expectedMessage = string.Format("The type '{0}' has already been registered", typeof(FakePhysicalFileOverHttp).FullName);
            ThenExceptionMessageShouldEqual(ex, expectedMessage);
        }

        private void ThenRuleAtIndexShouldHaveFileModelTypeOf(int index, Type expectedType)
        {
            FileMapping.PhysicalFilePathMappingRules
                       .ElementAt(index)
                       .FileModelType
                       .Should()
                       .Equal(expectedType);
        }
    }
}
