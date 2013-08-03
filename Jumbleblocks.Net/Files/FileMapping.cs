using System.Collections.Generic;
using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Files
{
    public static class FileMapping
    {
        static FileMapping()
        {
            PhysicalFilePathMappingRules = new List<FileMappingRuleSet>();
        }

        public static IList<FileMappingRuleSet> PhysicalFilePathMappingRules { get; private set; }

        public static IPhysicalWithRule<TModelType> RegisterPhysicalFileRulesForType<TModelType>()
            where TModelType : IPhysicalFileOverHttp
        {
            var ruleGenerator = new PhysicalFileRuleGenerator<TModelType>();
            PhysicalFilePathMappingRules.Add(ruleGenerator.GetFileMappingRuleSet());

            return ruleGenerator.Start(); 
        }
    }
}
