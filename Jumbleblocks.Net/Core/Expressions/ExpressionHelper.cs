using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Jumbleblocks.Net.Core.Expressions
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// Gets member name
        /// </summary>
        /// <typeparam name="TClass">Type of class</typeparam>
        /// <typeparam name="TMember">Type underlying member</typeparam>
        /// <param name="member">expression to property</param>
        /// <returns>name of specified member</returns>
        public static string GetMemberName<TClass, TMember>(this Expression<Func<TClass, TMember>> member)
        {
            if (member.Body is MethodCallExpression)
            {
                var methodName = member.GetMethodName();

                throw new MemberExpressionException(methodName, typeof(TClass),
                                                    string.Format("'{0}' is not a member, it is a method on '{1}'", methodName, typeof(TClass).FullName));
            }

            var expression = (MemberExpression)member.Body;
            return expression.Member.Name;
        }

        public static string GetMethodName<TClass, TMethod>(this Expression<Func<TClass, TMethod>> method)
        {
            var expression = (MethodCallExpression) method.Body;
            return expression.Method.Name;
        }

        /// <summary>
        /// determines if member is a property
        /// </summary>
        /// <typeparam name="TClass">Type of class</typeparam>
        /// <typeparam name="TMember">Type underlying member</typeparam>
        /// <param name="member">expression to property</param>
        /// <returns>true if property, otherwise false</returns>
        public static bool IsProperty<TClass, TMember>(this Expression<Func<TClass, TMember>> member)
        {
            var expression = (MemberExpression)member.Body;
            return expression.Member.MemberType == MemberTypes.Property;
        }

        /// <summary>
        /// Gets property info off a member
        /// </summary>
        /// <typeparam name="TClass">Type of class</typeparam>
        /// <typeparam name="TProperty">Type underlying propery</typeparam>
        /// <param name="property">expression to property</param>
        /// <param name="bindingFlags">reflection binding flags to use to find property on TClass</param>
        /// <returns>name of specified member</returns>
        public static PropertyInfo GetPropertyInfo<TClass, TProperty>(this Expression<Func<TClass, TProperty>> property, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
        {
            var classType = typeof (TClass);

            var memberName = property.GetMemberName();

            if (property.IsProperty())
                return classType.GetProperty(memberName, bindingFlags);

            throw new MemberExpressionException(memberName, classType, string.Format("'{0}' is not a property on '{1}'", property.GetMemberName(), classType));
        }

    }
}
