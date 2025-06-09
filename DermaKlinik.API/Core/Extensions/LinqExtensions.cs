using System.Linq.Expressions;
using System.Reflection;

namespace DermaKlinik.API.Core.Extensions
{
    public static class LinqExtensions
    {
        public static PropertyInfo ExtractPropertyInfo(this LambdaExpression propertyAccessor)
        {
            return propertyAccessor.ExtractMemberInfo() as PropertyInfo;
        }

        public static FieldInfo ExtractFieldInfo(this LambdaExpression propertyAccessor) => propertyAccessor.ExtractMemberInfo() as FieldInfo;

        public static MemberInfo ExtractMemberInfo(this LambdaExpression propertyAccessor)
        {
            MemberInfo member;
            try
            {
                LambdaExpression lambdaExpression = propertyAccessor;
                member = (!(lambdaExpression.Body is UnaryExpression) ? (MemberExpression)lambdaExpression.Body : (MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand).Member;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("The property or field accessor expression is not in the expected format 'o => o.PropertyOrField'.", ex);
            }
            return member;
        }

        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            PropertyInfo propertyInfo = objType.GetProperties().FirstOrDefault(p => p.Name == name);
            return propertyInfo == null ? null : propertyInfo;
        }

        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            ParameterExpression parameterExpression = Expression.Parameter(objType);
            return Expression.Lambda(Expression.PropertyOrField(parameterExpression, pi.Name), parameterExpression);
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string fieldName, bool descending = false)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(typeof(T), fieldName);
            if (propertyInfo == null)
                return query;
            LambdaExpression orderExpression = GetOrderExpression(typeof(T), propertyInfo);
            return (IEnumerable<T>)(descending
                ? typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2)
                : typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == nameof(OrderBy) && m.GetParameters().Length == 2)).MakeGenericMethod(typeof(T), propertyInfo.PropertyType).Invoke(null, new object[2] { query, orderExpression.Compile() });
        }
    }
}
