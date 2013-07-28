using Jumbleblocks.Net.Formatting;
using NUnit.Framework;

namespace Tests.Jumbleblocks.Net.Formatting.FileMediaTypeFormatterTests
{
    public class FileMediaTypeFormatterTestBase : TestBase<FileMediaTypeFormatter>
    {
        [SetUp]
        public void SetUp()
        {
            ItemUnderTest = new FileMediaTypeFormatter();
            SetUp_AfterItemUnderTest();
        }

        public virtual void SetUp_AfterItemUnderTest()
        {
        }
    }
}
