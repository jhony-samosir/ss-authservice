using Ganss.Xss;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections;
using System.Reflection;

namespace SS.AuthService.API.Filters;

/// <summary>
/// Action Filter untuk membersihkan input string (Query & Route Params) dari karakter berbahaya.
/// Catatan: Sanitasi Body (JSON) sudah ditangani oleh SanitizedStringConverter.
/// </summary>
public class SanitizeInputFilter : IAsyncActionFilter
{
    private static readonly HtmlSanitizer Sanitizer = new();

    static SanitizeInputFilter()
    {
        Sanitizer.AllowedTags.Clear();
        Sanitizer.AllowedAttributes.Clear();
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var visited = new HashSet<object>();

        // Gunakan ToList() agar bisa memodifikasi dictionary saat iterasi
        foreach (var argument in context.ActionArguments.ToList())
        {
            if (argument.Value is string strValue)
            {
                // Karena string immutable, timpa nilai langsung di dictionary
                context.ActionArguments[argument.Key] = Sanitizer.Sanitize(strValue);
            }
            else if (argument.Value != null)
            {
                SanitizeObject(argument.Value, visited);
            }
        }

        await next();
    }

    private void SanitizeObject(object? obj, HashSet<object> visited)
    {
        if (obj == null || visited.Contains(obj)) return;
        
        var type = obj.GetType();
        if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime)) return;

        visited.Add(obj);

        if (obj is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                if (item != null) SanitizeObject(item, visited);
            }
            return;
        }

        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .Where(p => p.CanRead && p.CanWrite);

        foreach (var prop in properties)
        {
            try
            {
                var value = prop.GetValue(obj);
                if (value == null) continue;

                if (prop.PropertyType == typeof(string))
                {
                    var originalValue = (string)value;
                    var sanitizedValue = Sanitizer.Sanitize(originalValue);
                    if (originalValue != sanitizedValue)
                    {
                        prop.SetValue(obj, sanitizedValue);
                    }
                }
                else if (prop.PropertyType.IsClass)
                {
                    SanitizeObject(value, visited);
                }
            }
            catch
            {
                // Ignore properties that cannot be accessed
            }
        }
    }
}
