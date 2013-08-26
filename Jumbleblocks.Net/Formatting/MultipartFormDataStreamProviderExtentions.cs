using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;

namespace Jumbleblocks.Net.Formatting
{
    public static class MultipartFormDataStreamProviderExtentions
    {
        public static object ReadFormDataAs(this MultipartFormDataStreamProvider provider, Type type)
        {
            var obj = GetFormDataAsFormDataCollection(provider).ReadAs(type);

            return obj ?? Activator.CreateInstance(type);
        }

        private static FormDataCollection GetFormDataAsFormDataCollection(MultipartFormDataStreamProvider provider)
        {
            var keysAndValues = (from key in provider.FormData.AllKeys
                                 from val in provider.FormData.GetValues(key)
                                 select new KeyValuePair<string, string>(key, val)).ToList();

            return new FormDataCollection(keysAndValues);
        }
    }
}
