using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Files
{
    public interface IFileBuildRule<TFileModel>
        where TFileModel : IFileOverHttp
    {
        IFilePathRule<TFileModel> AddFilePathRule { get; }
        IFileBuildRule<TFileModel> DefaultSavePath(string path);
    }
}
