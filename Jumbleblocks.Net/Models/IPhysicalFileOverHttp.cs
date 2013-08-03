namespace Jumbleblocks.Net.Models
{
    public interface IPhysicalFileOverHttp : IFileOverHttp
    {
         string FullFilePath { get; set; }
    }
}
