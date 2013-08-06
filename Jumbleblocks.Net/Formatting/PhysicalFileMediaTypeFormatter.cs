using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Jumbleblocks.Net.Core.Reflection;
using Jumbleblocks.Net.Files;
using Jumbleblocks.Net.Models;

namespace Jumbleblocks.Net.Formatting
{
    public class PhysicalFileMediaTypeFormatter: FileMediaTypeFormatter
    {
        public PhysicalFileMediaTypeFormatter(IFileMappingReader fileMappingReader)
        {
            _fileMappingReader = fileMappingReader;
        }

        private readonly IFileMappingReader _fileMappingReader;

        public override bool CanReadType(Type type)
        {
            return type.IsImplementationOf(typeof(IPhysicalFileOverHttp));
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public async override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            AssertContentIsMimeMultipartContent(content);
            var filePathMappingRules = GetAndAssertFilePathMappingRulesForType(type);

            //1: build model from form data
            //2: evaluate rules for file path and create MultipartFormDataStreamProvider
            //3: save file

            //TODO: implement custom  MultipartFormDataStreamProvider : MultipartFileStreamProvider
            //     so can evaluate form data and use it in file mapping rules
            //     off rule can get details or property to evaluate and match to Form Data
            //     can return type created from form data

           
            throw new NotImplementedException();
        }

        private FileMappingRuleSet GetAndAssertFilePathMappingRulesForType(Type type)
        {
            var filePathMappingRules = _fileMappingReader.PhysicalFilePathMappingRules.SingleOrDefault(x => x.FileModelType == type);

            if (filePathMappingRules == null)
                throw new FileRulesException(string.Format("Can not find file path mapping rules for type '{0}'", type.FullName));

            return filePathMappingRules;
        
        }

        //TODO: implement custom  MultipartFormDataStreamProvider : MultipartFileStreamProvider

        //TODO
          //catch (Exception e)
          //  {
          //      if (formatterLogger == null)
          //      {
          //          throw;
          //      }
          //      formatterLogger.LogError(String.Empty, e);
          //      return GetDefaultValueForType(type);
          //  }
    }
}
