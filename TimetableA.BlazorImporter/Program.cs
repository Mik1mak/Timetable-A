using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TimetableA.BlazorImporter;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRefitClient<ITimetableEndpoints>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri(@"https://timetablea-api.azurewebsites.net");
});

builder.Services.AddScoped(typeof(TimetableSender));

await builder.Build().RunAsync();
