namespace Tests.Jumbleblocks.Net
{
    /// <summary>
    /// Test context, provides central area to set parameters and call methods
    /// </summary>
    /// <typeparam name="TItemUnderTest">type of item under test</typeparam>
    public class TestBase<TItemUnderTest>
    {
        public TItemUnderTest ItemUnderTest { get; set; }
    }
}
