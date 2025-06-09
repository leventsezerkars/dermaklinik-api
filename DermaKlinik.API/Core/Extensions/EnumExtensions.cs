using DermaKlinik.API.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace DermaKlinik.API.Core.Extensions
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<TAttribute>();
        }

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
                return "";
            FieldInfo field = type.GetField(name);
            if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute customAttribute && !customAttribute.Description.IsEmpty())
                name = customAttribute.Description;
            return name;
        }

        public static IEnumerable<SelectListItem> ToSelectList(this Type type, SelectListValue valuefrom = SelectListValue.EnumValue)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (string name in type.GetFields().Where(fi => fi.IsStatic).OrderBy(fi => fi.MetadataToken).Select(m => m.Name))
            {
                string key = name;
                string str1 = "";
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute customAttribute1 && !customAttribute1.Description.IsEmpty())
                        key = customAttribute1.Description;

                    string str2 = "";
                    if (valuefrom == SelectListValue.EnumValue)
                        str2 = Convert.ToInt32(Enum.Parse(type, name)).ToString();
                    if (valuefrom == SelectListValue.EnumName)
                        str2 = name;
                    if (valuefrom == SelectListValue.EnumDescription)
                        str2 = key;
                    if (valuefrom == SelectListValue.EnumColor)
                        str2 = str1;
                    selectList.Add(new SelectListItem()
                    {
                        Text = key,
                        Value = str2
                    });
                }
            }
            return selectList;
        }

        public static int ToInt(this Enum val) => Convert.ToInt32(val);

        public static short ToShort(this Enum val) => Convert.ToInt16(val);

        public static string GetDescription<TEnum>(this string value, string fallback = "") where TEnum : struct
        {
            string description = fallback;
            TEnum result;
            if (!value.IsEmpty() && Enum.TryParse(value, true, out result) && Enum.IsDefined(typeof(TEnum), result.ToString()))
            {
                Type type = result.GetType();
                string name = Enum.GetName(typeof(TEnum), result);
                if (name != null)
                    description = name;
                FieldInfo field = type.GetField(name);
                if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute customAttribute && !customAttribute.Description.IsEmpty())
                    description = customAttribute.Description;
            }
            return description;
        }

        public static string GetDescription<TEnum>(this short value, string fallback = "") where TEnum : struct => value.ToString().GetDescription<TEnum>(fallback);

        public static string GetDescription<TEnum>(this short? value, string fallback = "") where TEnum : struct => (value.HasValue ? value.ToString() : null).GetDescription<TEnum>(fallback);

        public static string GetDescription<TEnum>(this int value, string fallback = "") where TEnum : struct => value.ToString().GetDescription<TEnum>(fallback);

        public static string GetDescription<TEnum>(this int? value, string fallback = "") where TEnum : struct => (value.HasValue ? value.ToString() : null).GetDescription<TEnum>(fallback);

        public static IEnumerable<string> GetDescriptionList(this Type type) => type.ToSelectList().Select(m => m.Text);

        public static string GetDescriptionByEnumName<TEnum>(this string enumname) where TEnum : Enum => enumname.IsEmpty() ? "" : ((TEnum)Enum.Parse(typeof(TEnum), enumname)).GetDescription();

        public static T GetIdToEnum<T>(this string enumval) where T : Enum => (T)Enum.Parse(typeof(T), enumval);

    }
}
