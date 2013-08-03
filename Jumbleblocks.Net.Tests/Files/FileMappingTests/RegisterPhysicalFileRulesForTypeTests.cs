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
        [Test]
        public void AddsNewFileMappingRule_ToRulesForSpecifiedType_AndReturnsPhysicalFileRuleGenerator()
        {
            var ruleGenerator = FileMapping.RegisterPhysicalFileRulesForType<FakePhysicalFileOverHttp>();

            //TODO: test only register type once
                       

            ThenObjectShouldBeOfType<PhysicalFileRuleGenerator<FakePhysicalFileOverHttp>>(ruleGenerator);
            ThenEnumerationShouldCountExactly(FileMapping.PhysicalFilePathMappingRules, 1);
            ThenRuleAtIndexShouldHaveFileModelTypeOf(0, typeof (FakePhysicalFileOverHttp));
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
