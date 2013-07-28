﻿using Jumbleblocks.Net.Models;
using NUnit.Framework;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net.Formatting.FileMediaTypeFormatterTests
{
    [TestFixture]
    public class CanReadTests : FileMediaTypeFormatterTestBase
    {
        [Test]
        public void WithTypeFileOverHttp_ShouldReturnTrue()
        {
            ItemUnderTest.CanReadType(typeof(FileOverHttp)).Should().Be.True();
        }

        [Test]
        public void WithTypeFilePartOverHttp_ShouldReturnTrue()
        {
            ItemUnderTest.CanReadType(typeof(FilePartOverHttp)).Should().Be.True();
        }
    }
}