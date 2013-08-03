using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Files
{
    public interface IPhysicalWithRule<TFileModel>
        where TFileModel : IPhysicalFileOverHttp
    {
        IPhysicalFilePathRule<TFileModel> AddFilePathRule { get; }
        IPhysicalWithRule<TFileModel> DefaultSavePath(string path);
    }
}
