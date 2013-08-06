﻿using Jumbleblocks.Net.Formatting;
using NUnit.Framework;
using Should.Fluent;
using Tests.Jumbleblocks.Net.Helpers;

namespace Tests.Jumbleblocks.Net.Formatting.PhysicalFileMediaTypeFormatterTests
{
    [TestFixture]
    public class SupportedMediaTypesTests : TestBase<PhysicalFileMediaTypeFormatter>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new PhysicalFileMediaTypeFormatter(new FileMappingReaderMock().Object);
        }

        [Test]
        public void Supports_OctetStream_MediaType()
        {
            ItemUnderTest.SupportedMediaTypes.Should().Contain.One(mdh => mdh.MediaType == "application/octet-stream");
        }

        [Test]
        public void Supports_FormData_MediaType()
        {
            ItemUnderTest.SupportedMediaTypes.Should().Contain.One(mdh => mdh.MediaType == "multipart/form-data");
        }
    }
}