using System;
using System.Linq.Expressions;
using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Files
{
    public interface IFilePathRule<TFileModel>
        where TFileModel : IFileOverHttp
    {
        IFileBuildRule<TFileModel> WhenPropertyMatchesSaveTo<TProperty>(Expression<Func<TFileModel, TProperty>> property, Func<TProperty, bool> evaluator, string saveToFilePath);
    }
}
