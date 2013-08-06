using System;

namespace Jumbleblocks.Net.Core.Reflection
{
    public static class TypeExtensions
    {
        public static bool IsImplementationOf(this Type type, Type interfaceType)
        {
            return type.GetInterface(interfaceType.FullName) != null;
        }
    }
}
