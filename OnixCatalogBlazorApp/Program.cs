using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using OnixCatalogBlazorApp;
using OnixCatalogBlazorApp.Proxy;
using OnixCatalogBlazorApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CacheStorageProxy>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();

await builder.Build().RunAsync();
