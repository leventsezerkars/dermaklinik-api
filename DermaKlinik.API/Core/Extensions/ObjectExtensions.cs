using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace DermaKlinik.API.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static bool TryCast<T>(this object obj, out T result)
        {
            if (obj is T obj1)
            {
                result = obj1;
                return true;
            }
            result = default;
            return false;
        }

        public static T Cast<T>(this object obj, T result = default)
        {
            if (obj is T obj1)
                result = obj1;
            return result;
        }

        public static IEnumerable<string> AsArray(this object obj)
        {
            if (obj == null)
                return null;
            IEnumerable<string> result1;
            if (obj.TryCast(out result1))
                return result1;
            obj.TryCast(out string result2);
            return new string[1] { result2 };
        }

        public static T Default<T>(this T? src) where T : struct
        {
            double? result;
            return src.TryCast(out result) && result.HasValue && double.IsNaN(result.Value) ? default : src.GetValueOrDefault();
        }

        public static T Default<T>(this T? src, T def) where T : struct
        {
            double? result;
            return src.TryCast(out result) && result.HasValue && double.IsNaN(result.Value) ? def : src ?? def;
        }

        public static IDictionary<string, object> ToDictionary(this object @object, bool replaceUnderscores = true)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
            if (@object != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(@object))
                {
                    string key = property.Name;
                    if (replaceUnderscores)
                        key = property.Name.Replace("_", "-");
                    dictionary.Add(key, property.GetValue(@object));
                }
            }
            return dictionary;
        }

        public static object GetPropValue(this object obj, string name)
        {
            foreach (string name1 in name.Split("."))
            {
                if (obj == null)
                    return null;
                PropertyInfo property = obj.GetType().GetProperty(name1);
                if (property == null)
                    return null;
                obj = property.GetValue(obj, (object[])null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this object obj, string name)
        {
            object propValue = obj.GetPropValue(name);
            return propValue == null ? default : (T)propValue;
        }

        public static bool IsPublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self == null || to == null)
                return self == to;
            Type type = typeof(T);
            List<string> stringList = new List<string>(ignore);
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!stringList.Contains(property.Name))
                {
                    object obj1 = type.GetProperty(property.Name).GetValue(self, (object[])null);
                    object obj2 = type.GetProperty(property.Name).GetValue(to, (object[])null);
                    if (obj1 != obj2 && (obj1 == null || !obj1.Equals(obj2)))
                        return false;
                }
            }
            return true;
        }

        public static List<T> ConvertData<T>(this T self, DataTable data) where T : new()
        {
            List<T> objList = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (DataRow row in (InternalDataCollectionBase)data.Rows)
            {
                T obj1 = new T();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    if (!(propertyInfo.PropertyType != typeof(string)))
                    {
                        var obj2 = propertyInfo.GetValue(self);
                        if (obj2 != null)
                            propertyInfo.SetValue(obj1, row[int.Parse(obj2.ToString()) - 1].ToString());
                    }
                }
                objList.Add(obj1);
            }
            return objList;
        }

        public static string Serialize(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
