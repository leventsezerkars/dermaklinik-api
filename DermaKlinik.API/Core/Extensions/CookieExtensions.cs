using Newtonsoft.Json;

namespace DermaKlinik.API.Core.Extensions
{
    public class CookieExtensions
    {
        private readonly HttpContext _httpContext;

        public CookieExtensions(HttpContext httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
        }
        public string Get(string key)
        {
            try
            {
                var s = _httpContext.Request.Cookies[key];
                return s ?? "";
            }
            catch (Exception)
            {
                return "";
            }

        }
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
            {
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            }
            else
            {
                option.Expires = DateTime.Now.AddMilliseconds(10);
            }

            _httpContext.Response.Cookies.Append(key, value, option);
        }

        public void Remove(string key)
        {
            _httpContext.Response.Cookies.Delete(key);
        }

        public void SetObject<T>(string key, T value, int? expireTime) where T : class
        {
            Set(key, JsonConvert.SerializeObject(value), expireTime);
        }

        public T GetObject<T>(string key) where T : class
        {
            return JsonConvert.DeserializeObject<T>(Get(key));
        }
    }
}
