//note check out http://lonetechie.com/2012/09/23/web-api-generic-mediatypeformatter-for-file-upload/


namespace Jumbleblocks.Net.Models
{
    /// <summary>
    /// represents a file
    /// </summary>
    public abstract class FileOverHttpBase
    {
        public string FileName { get; set; }
        public string MediaType { get; set; }
    }
}
