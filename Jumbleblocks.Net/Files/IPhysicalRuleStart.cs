using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Files
{
    public interface IPhysicalRuleStart<TFileModel>
        where TFileModel : IPhysicalFileOverHttp
    {
        IPhysicalWithRule<TFileModel> Start();
    }
}
