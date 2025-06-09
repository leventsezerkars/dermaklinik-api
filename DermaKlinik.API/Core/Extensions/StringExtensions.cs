using Newtonsoft.Json;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System;

namespace DermaKlinik.API.Core.Extensions
{
    public static class StringExtensions
    {

        public static string JavascriptStringEncode(this string src, bool forsinglequote = true)
        {
            char ch = '\'';
            if (!forsinglequote)
                ch = '"';
            string str = JsonConvert.ToString(src.Coalesce(), ch);
            return str.Substring(1, str.Length - 2);
        }

        public static string EncodeBase64(this string src) => src == null ? null : Convert.ToBase64String(Encoding.UTF8.GetBytes(src));

        public static string DecodeBase64(this string src) => src == null ? null : Encoding.UTF8.GetString(Convert.FromBase64String(src));

        public static string SafeSubstring(this string src, int length)
        {
            src = src.Coalesce();
            return src.Substring(0, src.Length <= length || length <= 0 ? src.Length : length);
        }

        public static string SafeSubstring(this string src, int startIndex, int length)
        {
            if (src == null)
                return null;
            return startIndex > 0 ? src.Substring(startIndex, src.Length - startIndex > length ? length : src.Length - startIndex) : src.SafeSubstring(length);
        }

        public static string Repeat(this string src, int count) => string.Concat(Enumerable.Repeat(src, count));

        public static string[] Split(this string src, string delim)
        {
            src = src.Coalesce();
            return src.Split(new string[1] { delim }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] Split(this string src, string[] delims)
        {
            src = src.Coalesce();
            return src.Split(delims, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> Split(this string src, int chunkSize) => src.Coalesce().ChunkBy(chunkSize).Select(m => string.Join("", m));

        public static bool IsEmpty(this string value) => string.IsNullOrWhiteSpace(value);

        public static bool IsCaseSensitiveEqual(this string value, string comparing) => string.CompareOrdinal(value, comparing) == 0;

        public static string Coalesce(this string input) => input ?? "";

        public static string Coalesce(this string input, string def) => input.Coalesce().IsEmpty() ? def : input.Coalesce();

        public static string EscapeMySQLQuotes(this string input) => input.Coalesce().Replace("\\", "\\\\").Replace("'", "\\'");

        public static string FormatInvariant(this string instance, params object[] args) => string.Format(CultureInfo.InvariantCulture, instance, args);



        public static string ReplaceTurkishCharsToAscii(this string str)
        {
            return str.Replace('İ', 'I')
                .Replace('ı', 'i')
                .Replace('Ü', 'U')
                .Replace('ü', 'u')
                .Replace('Ş', 'S')
                .Replace('ş', 's')
                .Replace('Ö', 'O')
                .Replace('ö', 'o')
                .Replace('Ç', 'C')
                .Replace('ç', 'c')
                .Replace('Ğ', 'G')
                .Replace('ğ', 'g');
        }

        public static string Reverse(this string value)
        {
            char[] charArray = value.ToCharArray();
            Array.Reverse((Array)charArray);
            return new string(charArray);
        }

        public static string TrimEnd(this string input, string suffix) => input != null && suffix != null && input.EndsWith(suffix) ? input.Substring(0, input.Length - suffix.Length) : input;

        public static string TrimStart(this string input, string prefix) => input != null && prefix != null && input.StartsWith(prefix) ? input.Substring(prefix.Length) : input;

        public static string StripNonAscii(this string input) => Regex.Replace(input, "[^\\u0000-\\u007F]+", "");

        public static string SplitTitleCase(this string input) => string.Join(" ", input.Split("_").Select(s => s[0].ToString().ToUpper() + s.Substring(1).ToLower()));

        public static string ToTitleCase(this string str) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());

        public static short ToShort(this string val, short defaultValue = 0)
        {
            return short.TryParse(val, out short result) ? result : defaultValue;
        }

        public static int ToInt(this string val, int defaultValue = 0)
        {
            return int.TryParse(val, out int result) ? result : defaultValue;
        }

        public static long ToLong(this string val, long defaultValue = 0)
        {
            return long.TryParse(val, out long result) ? result : defaultValue;
        }

        public static double ToDouble(this string val, double defaultValue = 0.0)
        {
            return double.TryParse(val, out double result) ? result : defaultValue;
        }

        public static string FirstCharToUpper(this string input)
        {
            return input switch
            {
                "" => "",
                null => throw new ArgumentNullException(nameof(input)),
                _ => string.Concat(input.First().ToString().ToUpperInvariant(), input.AsSpan(1)),
            };
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp) => source != null && source.IndexOf(toCheck, comp) >= 0;

        public static long ToInt64(this string value, int defValue = 0)
        {
            var res = long.TryParse(value, out long result);
            if (res)
                return result;

            return defValue;
        }

        public static decimal ToDecimal(this string value, decimal defValue = 0)
        {
            var res = decimal.TryParse(value, out decimal result);
            if (res)
                return result;

            return defValue;
        }

        public static bool ToBool(this string value, bool defValue = false)
        {
            var res = bool.TryParse(value, out bool result);
            if (res)
                return result;

            return defValue;
        }

        public static string Slug(this string text)
        {
            if (text == null) text = "";
            var unaccentedText = string.Join("", text.ToLower().ReplaceTurkishCharsToAscii().Normalize(NormalizationForm.FormD)
                .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            return System.Web.HttpUtility.UrlEncode(unaccentedText.Replace("'", "").Replace(" ", "-")).Replace("+", "");
        }

        public static string BetweenString(this string text, string firstv = "{", string lastv = "}")
        {
            if (text != null && text.Length > 0)
            {
                int first = text.IndexOf(firstv) + firstv.Length;
                int last = text.LastIndexOf(lastv);
                string str2 = text.Substring(first, last - first);
                return str2;
            }
            return text;
        }


        public static string Md5(this string stringToHash)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in hash)
                stringBuilder.Append(num.ToString("x2").ToLower());
            return stringBuilder.ToString();
        }

        public static T Deserialize<T>(this string obj)
        {
            if (obj != null)
                return JsonConvert.DeserializeObject<T>(obj);

            return default;
        }

        public static string Sifrele(this string text)
        {
            string hash = "selcumselm";
            byte[] data = Encoding.UTF8.GetBytes(text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        public static string Coz(this string text)
        {
            string hash = "selcumselm";
            byte[] data = Convert.FromBase64String(text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.UTF8.GetString(results);
                }
            }
        }
    }
}
