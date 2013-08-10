using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Jumbleblocks.Net.Formatting;
using NUnit.Framework;
using Should.Fluent;
using Tests.Jumbleblocks.Net.Files;

namespace Tests.Jumbleblocks.Net.Formatting.MultipartFormDataStreamProviderExtentionsTests
{
    [TestFixture]
    public class ReadFormDataAsTests : TestBase
    {
        private MultipartFormDataStreamProvider _streamProvider;

        [SetUp]
        public void SetUp()
        {
            _streamProvider = new MultipartFormDataStreamProvider("~/App_Data/");
        }

        [Test]
        public void WithTypeFakeFileOverHttp2_AndFormDataContainsValueFor_PropertySetByModelBinding_ThenReturnsNewModelWithPropertySet()
        {
            const string expectedValue = "Test123";
            var expectedType = typeof (FakeFileOverHttp2);

            _streamProvider.FormData.Add("PropertySetByModelBinding", expectedValue);
            var boundObject = MultipartFormDataStreamProviderExtentions.ReadFormDataAs(_streamProvider, expectedType);
            
            ThenObjectShouldBeOfType(boundObject, expectedType);
            ThenPropertyShouldEqual((FakeFileOverHttp2)boundObject, x => x.PropertySetByModelBinding, expectedValue);
        }

        [Test]
        public void WithTypeFakeFileOverHttp2_AndFormDataContainsDoesNotValueFor_PropertySetByModelBinding_ThenReturnsNewModelWithoutPropertySet()
        {
            var expectedType = typeof(FakeFileOverHttp2);

            var boundObject = MultipartFormDataStreamProviderExtentions.ReadFormDataAs(_streamProvider, expectedType);

            ThenObjectShouldBeOfType(boundObject, expectedType);
            ThenPropertyShouldBeNull(((FakeFileOverHttp2) boundObject), x => x.PropertySetByModelBinding);
        }
    }
}
