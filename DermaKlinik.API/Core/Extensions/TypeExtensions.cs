using DermaKlinik.API.Core.Extensions;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace DermaKlinik.API.Core.Extensions
{
    public static class TypeExtensions
    {
        public static readonly Dictionary<Type, string> TypeAliases = new Dictionary<Type, string>()
        {
            {typeof (int), "int"},
            {typeof (short), "short"},
            {typeof (byte),"byte"},
            {typeof (byte[]), "byte[]"},
            {typeof (long), "long"},
            {typeof (double), "double"},
            {typeof (decimal),"decimal"},
            {typeof (float), "float"},
            {typeof (bool), "bool"},
            {typeof (string), "string"},
            {typeof (TimeSpan), "TimeSpan"}
        };
        public static readonly Type[] PredefinedTypes = new Type[20]
        {
            typeof (object),
            typeof (bool),
            typeof (char),
            typeof (string),
            typeof (sbyte),
            typeof (byte),
            typeof (short),
            typeof (ushort),
            typeof (int),
            typeof (uint),
            typeof (long),
            typeof (ulong),
            typeof (float),
            typeof (double),
            typeof (decimal),
            typeof (DateTime),
            typeof (TimeSpan),
            typeof (Guid),
            typeof (Math),
            typeof (Convert)
        };

        public static bool IsSimpleType(this Type type)
        {
            int num;
            if (!type.IsValueType && !type.IsPrimitive)
            {
                if (!(new Type[6]
                {
                      typeof (string),
                      typeof (decimal),
                      typeof (DateTime),
                      typeof (DateTimeOffset),
                      typeof (TimeSpan),
                      typeof (Guid)
                }).Contains(type))
                {
                    num = Convert.GetTypeCode(type) != TypeCode.Object ? 1 : 0;
                    goto label_4;
                }
            }
            num = 1;
        label_4:
            return num != 0;
        }

        public static IEnumerable<PropInfo> GetNestedPropertyNamesSimple(this Type type, string prefix = "", int level = 0, HashSet<Type> visitedTypes = null)
        {
            ++level;
            if (visitedTypes == null)
                visitedTypes = new HashSet<Type>();
            if (!visitedTypes.Contains(type))
            {
                visitedTypes.Add(type);
                PropertyInfo[] propertyInfoArray = type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                for (int index = 0; index < propertyInfoArray.Length; ++index)
                {
                    PropertyInfo item = propertyInfoArray[index];
                    if (item.PropertyType.IsSimpleType())
                    {
                        Type type1 = Nullable.GetUnderlyingType(item.PropertyType);
                        if ((object)type1 == null)
                            type1 = item.PropertyType;
                        Type propertyType = type1;
                        string typename = TypeAliases.ContainsKey(propertyType) ? TypeAliases[propertyType] : propertyType.Name;
                        if (Nullable.GetUnderlyingType(item.PropertyType) != null)
                            typename += "?";
                        bool writable = true;
                        if (item.GetSetMethod() == null)
                            writable = false;
                        yield return new PropInfo()
                        {
                            PropertyName = prefix + item.Name,
                            TypeName = typename,
                            Writable = writable
                        };
                        propertyType = null;
                        typename = null;
                    }
                    else if (typeof(IEnumerable).IsAssignableFrom(item.PropertyType))
                    {
                        if (level <= 5)
                        {
                            Type generic = item.PropertyType.GetInnerGenericType();
                            foreach (PropInfo sub in generic.GetNestedPropertyNamesSimple(item.Name + ".", level, visitedTypes))
                                yield return sub;
                            generic = null;
                        }
                    }
                    else if (level <= 5)
                    {
                        foreach (PropInfo sub in item.PropertyType.GetNestedPropertyNamesSimple(item.Name + ".", level, visitedTypes))
                            yield return sub;
                    }
                    item = null;
                }
                propertyInfoArray = null;
            }
        }

        public static Type GetInnerGenericType(this Type type)
        {
            Type type1 = type.GetGenericArguments().FirstOrDefault();
            return (object)type1 == null ? type : type1.GetInnerGenericType();
        }



        public static string GetDisplayName(this PropertyInfo p, bool includeAlias = false)
        {
            string displayName = p.Name;
            DisplayNameAttribute attributeWithMetadata1 = p.GetCustomAttribute<DisplayNameAttribute>();
            if (attributeWithMetadata1 != null)
            {
                displayName = attributeWithMetadata1.DisplayName;
                if (attributeWithMetadata1 is DisplayNameAttribute displayNameAttribute)
                    displayName = displayNameAttribute.DisplayName;
            }
            return displayName;
        }

        public static bool IsPredefinedType(this Type type)
        {
            foreach (Type predefinedType in PredefinedTypes)
            {
                if (predefinedType == type)
                    return true;
            }
            return false;
        }

        public static bool IsEnumType(this Type type) => type.GetNonNullableType().IsEnum;

        public static Type GetNonNullableType(this Type type) => type.IsNullableType() ? type.GetGenericArguments()[0] : type;

        public static bool IsNullableType(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        public static bool IsSubClass(this Type type, Type check) => type.IsSubClass(check, out Type _);

        public static bool IsSubClass(this Type type, Type check, out Type implementingType) => IsSubClassInternal(type, type, check, out implementingType);

        private static bool IsSubClassInternal(Type initialType, Type currentType, Type check, out Type implementingType)
        {
            if (currentType == check)
            {
                implementingType = currentType;
                return true;
            }
            if (check.IsInterface && (initialType.IsInterface || currentType == initialType))
            {
                foreach (Type currentType1 in currentType.GetInterfaces())
                {
                    if (IsSubClassInternal(initialType, currentType1, check, out implementingType))
                    {
                        if (check == implementingType)
                            implementingType = currentType;
                        return true;
                    }
                }
            }
            if (currentType.IsGenericType && !currentType.IsGenericTypeDefinition && IsSubClassInternal(initialType, currentType.GetGenericTypeDefinition(), check, out implementingType))
            {
                implementingType = currentType;
                return true;
            }
            if (!(currentType.BaseType == null))
                return IsSubClassInternal(initialType, currentType.BaseType, check, out implementingType);
            implementingType = null;
            return false;
        }

        public static int ToInt(this object veri)
        {
            if (veri == null)
                veri = 0;
            return int.TryParse(veri.ToString(), out int result) ? result : 0;
        }

        public static short ToShort(this object veri)
        {
            if (veri == null)
                veri = 0;
            return short.TryParse(veri.ToString(), out short result) ? result : (short)0;
        }

        public static decimal ToDecimal(this object veri)
        {
            return decimal.TryParse(veri.ToString(), out decimal result) ? result : 0M;
        }

        public static float ToFloat(this object veri)
        {
            float result;
            float f = float.TryParse(veri.ToString(), out result) ? result : 0.0f;
            if (float.IsNaN(f) || float.IsInfinity(f))
                f = 0.0f;
            return f;
        }

        public static double ToDouble(this object veri)
        {
            if (veri == null)
                veri = 0;
            double result;
            double d = double.TryParse(veri.ToString(), out result) ? result : 0.0;
            if (double.IsNaN(d) || double.IsInfinity(d))
                d = 0.0;
            return d;
        }

        public static DateTime ToDateTime(this object veri)
        {
            if (veri == null)
                veri = new DateTime();
            DateTime result;
            return DateTime.TryParse(veri.ToString(), out result) ? result : result;
        }

        public static bool ToBool(this object veri)
        {
            if (veri == null || veri.ToString().IsEmpty())
                veri = false;
            return Convert.ToBoolean(veri);
        }

        public static string ToDate(this object veri)
        {
            if (veri == null)
                veri = new DateTime();
            DateTime result;
            return DateTime.TryParse(veri.ToString(), out result) ? result.ToString("dd.MM.yyyy") : result.ToString("dd.MM.yyyy");
        }

        public static long ToLong(this object veri)
        {
            if (veri == null)
                veri = 0;
            long result;
            return long.TryParse(veri.ToString(), out result) ? result : result;
        }

        public static TimeSpan ToTimeSpan(this object veri)
        {
            TimeSpan result;
            return TimeSpan.TryParse(veri.ToString(), out result) ? result : result;
        }

        public static bool IsNullDate(this string value)
        {
            string str1 = value;
            DateTime dateTime = new DateTime();
            dateTime = dateTime.Date;
            string str2 = dateTime.ToString("dd.MM.yyyy");
            if (str1 == str2)
            {
                value = null;
            }
            else
            {
                string str3 = value;
                dateTime = new DateTime();
                string str4 = dateTime.ToString();
                if (str3 == str4)
                    value = null;
            }
            return value == null;
        }

        public static string ToString(this object veri, Type tip, string formatTipi)
        {
            Type type = tip;
            if (type == typeof(int) || type == typeof(int))
                return veri.ToInt().ToString(formatTipi);
            if (type == typeof(decimal) || type == typeof(double))
                return veri.ToDecimal().ToString(formatTipi);
            if (type == typeof(string))
                return veri.ToString();
            if (type == typeof(DateTime))
                return veri.ToDateTime().ToString(formatTipi);
            if (type == typeof(float))
                return veri.ToFloat().ToString(formatTipi);
            if (type == typeof(long))
                return veri.ToLong().ToString(formatTipi);
            return type == typeof(TimeSpan) ? veri.ToTimeSpan().ToString(formatTipi) : veri.ToString();
        }

        public class PropInfo
        {
            public string PropertyName { get; set; }

            public string OriginName { get; set; }

            public string DisplayName { get; set; }

            public string TypeName { get; set; }

            public string FwTypeName { get; set; }

            public bool Writable { get; set; }

            public bool IgnoreFilterPanel { get; set; }

            public bool Required { get; set; }
        }
    }
}
