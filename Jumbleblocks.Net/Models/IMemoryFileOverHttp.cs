namespace Jumbleblocks.Net.Models
{
    public interface IMemoryFileOverHttp : IFileOverHttp
    {
        byte[] Buffer { get; set; }
    }
}
