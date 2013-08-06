using System;
using System.Collections.Generic;
using System.Linq;
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
            AssertModelTypeNotRegistered<TModelType>();

            var ruleGenerator = new PhysicalFileRuleGenerator<TModelType>();
            PhysicalFilePathMappingRules.Add(ruleGenerator.GetFileMappingRuleSet());

            return ruleGenerator.Start(); 
        }

        private static void AssertModelTypeNotRegistered<TModelType>()
        {
            var modelType = typeof (TModelType);

            if (PhysicalFilePathMappingRules.All(x => x.FileModelType != modelType)) return;

            var message = string.Format("The type '{0}' has already been registered", modelType.FullName);
            throw new DuplicateRegistrationException(message);
        }

        public static ValidationResultCollection ValidateRules()
        {
            var resultCollection = new ValidationResultCollection();
            resultCollection.AddRange(PhysicalFilePathMappingRules.Select(x => x.IsValid()));
            return resultCollection;
        }

        //TODO: test and implement
        public static void AssertRulesAreValid()
        {
            throw new NotImplementedException();
        }
    }
}
