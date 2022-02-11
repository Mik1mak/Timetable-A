using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TimetableA.BlazorImporter;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRefitClient<ITimetableEndpoints>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ApiUrl"]!);
});

builder.Services.AddScoped(typeof(TimetableSender));
builder.Services.AddScoped(typeof(TimetableGetter));

await builder.Build().RunAsync();
