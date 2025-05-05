using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EGNValidation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<IEGNValidator, Validation>();

await builder.Build().RunAsync();
