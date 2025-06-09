using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections;

namespace DermaKlinik.API.Core.Extensions
{
    public static class ModelStateExtensions
    {
        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()).Where(m => m.Value.Length > 0);
            }
            return null;
        }

    }
}
