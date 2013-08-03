using System;
using System.IO;
using System.Linq.Expressions;
using Jumbleblocks.Net.Core.Expressions;
using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Files
{
    public class PhysicalFileRuleGenerator<TFileModel> : IPhysicalRuleStart<TFileModel>, 
                                                         IPhysicalWithRule<TFileModel>,
                                                         IPhysicalFilePathRule<TFileModel>
        where TFileModel : IPhysicalFileOverHttp
    {

        //TODO: add trailing / to file path
        //TODO: file renaming??
        //TODO: overwriting existing files?
        private readonly FileMappingRuleSet _ruleSet = new FileMappingRuleSet();

        public IPhysicalWithRule<TFileModel> Start()
        {
            _ruleSet.FileModelType = typeof (TFileModel);
            return this;
        }

        public IPhysicalFilePathRule<TFileModel> AddFilePathRule 
        {
            get { return this; }
        }

        public IPhysicalWithRule<TFileModel> DefaultSavePath(string path)
        {
            if(_ruleSet.HasDefaultFilePath())
                throw new FileMappingException("DefaultSavePath() can only be called once and it has already been called");

            AssertValidFilePath(path);
            _ruleSet.DefaultFilePath = path;
            return this;
        }

        private static void AssertValidFilePath(string path)
        {
            if (!IsValidFilePath(path))
                throw new InvalidFilePathMappingException(path);
        }

        private static bool IsValidFilePath(string filePath)
        {
            return filePath.IndexOfAny(Path.GetInvalidPathChars()) == -1;
        }

        public IPhysicalWithRule<TFileModel> WhenPropertyMatchesSaveTo<TProperty>(Expression<Func<TFileModel, TProperty>> property, Func<TProperty, bool> evaluator, string saveToFilePath)
        {
            AssertValidFilePath(saveToFilePath);

            try
            {
                dynamic filePathRule = CreateFilePathMappingRule(property, saveToFilePath);
                filePathRule.Evaluate = evaluator;

                _ruleSet.AddFilePathMappingRule(filePathRule);
            }
            catch (MemberExpressionException ex)
            {
                var msg = string.Format("Can only map to properties. '{0}' of type '{1}' is not a property.", ex.MemberName, ex.ClassType);
                throw new FileMappingException(msg, ex);
            }
            
            return this;
        }

        private static FilePathMappingRule CreateFilePathMappingRule<TProperty>(Expression<Func<TFileModel, TProperty>> property, string saveToFilePath)
        {
            return new FilePathMappingRule
                {
                    PropertyToEvaluate = property.GetPropertyInfo(),
                    SavePath = saveToFilePath
                };
        }

        public FileMappingRuleSet GetFileMappingRuleSet()
        {
            return _ruleSet;
        }
    }
}
