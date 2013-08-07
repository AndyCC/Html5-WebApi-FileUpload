using System.Web.Configuration;

namespace Jumbleblocks.Net.Core.Configuration
{
    public class WebConfigurationManagerWrapper : IWebConfiguration
    {
        public string GetApplicationSetting(string name)
        {
            return WebConfigurationManager.AppSettings[name];
        }
    }
}
