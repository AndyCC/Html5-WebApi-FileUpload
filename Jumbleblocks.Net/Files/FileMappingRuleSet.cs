using System;
using System.Collections.Generic;

namespace Jumbleblocks.Net.Files
{
    public class FileMappingRuleSet : IRuleSetValidity
    {
        public FileMappingRuleSet()
        {
            FilePathMappingRules = new List<FilePathMappingRule>();
        }

        public IList<FilePathMappingRule> FilePathMappingRules { get; set; }
        public string DefaultFilePath { get; set; }
        public Type FileModelType { get; set; }

        public void AddFilePathMappingRule(FilePathMappingRule rule)
        {
            FilePathMappingRules.Add(rule);
        }

        public bool HasDefaultFilePath()
        {
            return !String.IsNullOrWhiteSpace(DefaultFilePath);
        }

        public ValidationResult IsValid()
        {
            var errors = new List<string>();

            if(string.IsNullOrWhiteSpace(DefaultFilePath))
                errors.Add("DefaultFilePath is null or empty");

            if(FileModelType == null)
                errors.Add("FileModelType is null");

            return new ValidationResult(errors);
        }
    }
}
