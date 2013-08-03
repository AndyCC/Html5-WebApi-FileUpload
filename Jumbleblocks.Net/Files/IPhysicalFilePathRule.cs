using System;
using System.Linq.Expressions;
using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Files
{
    public interface IPhysicalFilePathRule<TFileModel>
        where TFileModel : IPhysicalFileOverHttp
    {
        IPhysicalWithRule<TFileModel> WhenPropertyMatchesSaveTo<TProperty>(Expression<Func<TFileModel, TProperty>> property, Func<TProperty, bool> evaluator, string saveToFilePath);
    }
}
