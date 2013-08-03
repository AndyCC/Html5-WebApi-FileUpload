using System;
using System.Collections.Generic;
using Should.Fluent;

namespace Tests.Jumbleblocks.Net
{
    /// <summary>
    /// Test context, provides central area to set parameters and call methods
    /// </summary>
    /// <typeparam name="TItemUnderTest">type of item under test</typeparam>
    public class TestBase<TItemUnderTest> : TestBase
    {
        public TItemUnderTest ItemUnderTest { get; set; }
    }

    public class TestBase
    {
        public void ThenExceptionMessageShouldEqual(Exception ex, string expectedMessage)
        {
            ex.Message.Should().Equal(expectedMessage);
        }

        public void ThenEnumerationShouldCountExactly<T>(IEnumerable<T> enumerable, int expectedCount)
        {
            enumerable.Should().Count.Exactly(expectedCount);
        }

        public void ThenObjectShouldBeOfType<TExpectedType>(object obj)
        {
            ThenObjectShouldBeOfType(obj, typeof(TExpectedType));
        }

        public void ThenObjectShouldBeOfType(object obj, Type expectedType)
        {
            obj.Should().Be.OfType(expectedType);
        }
    }
}
