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
            FilePathMappingRules = new List<FileMappingRuleSet>();
        }

        public static IList<FileMappingRuleSet> FilePathMappingRules { get; private set; }

        public static IFileBuildRule<TModelType> RegisterFileRulesForType<TModelType>()
            where TModelType : IFileOverHttp
        {
            AssertModelTypeNotRegistered<TModelType>();

            var ruleGenerator = new FileRuleGenerator<TModelType>();
            FilePathMappingRules.Add(ruleGenerator.GetFileMappingRuleSet());

            return ruleGenerator.Start(); 
        }

        private static void AssertModelTypeNotRegistered<TModelType>()
        {
            var modelType = typeof (TModelType);

            if (FilePathMappingRules.All(x => x.FileModelType != modelType)) return;

            var message = string.Format("The type '{0}' has already been registered", modelType.FullName);
            throw new DuplicateRegistrationException(message);
        }

        public static ValidationResultCollection ValidateRules()
        {
            var resultCollection = new ValidationResultCollection();
            resultCollection.AddRange(FilePathMappingRules.Select(x => x.IsValid()));
            return resultCollection;
        }

        //TODO: test and implement
        public static void AssertRulesAreValid()
        {
            throw new NotImplementedException();
        }
    }
}
