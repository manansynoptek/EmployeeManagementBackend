using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace Employee.WebApi.Extension
{
    public static class Extension
    {
        public static string GetModelStateErrors(this ModelStateDictionary modelState)
        {
            return string.Join("<br />", modelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
        }

        public static string SerializeToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
