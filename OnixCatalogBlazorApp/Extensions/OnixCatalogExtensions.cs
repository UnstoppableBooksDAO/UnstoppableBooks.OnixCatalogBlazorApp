using Newtonsoft.Json;

using OnixCatalogBlazorApp.Models;

namespace OnixCatalogBlazorApp.Extensions
{
    public static class OnixCatalogExtensions
    {
        public static string Serialize(this BookItem bookItem)
        {
            return JsonConvert.SerializeObject(bookItem);
        }
    }
}
