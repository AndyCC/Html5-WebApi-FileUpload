using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Files
{
    public interface IFileRuleStart<TFileModel>
        where TFileModel : IFileOverHttp
    {
        IFileBuildRule<TFileModel> Start();
    }
}
