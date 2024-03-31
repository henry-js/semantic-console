using Community.Extensions.Spectre.Cli.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using semantic_console.Cli.Commands;
using semantic_console.Lib.SemanticKernelSettings;
using Spectre.Console.Cli;
using semantic_console.Lib.Extensions;


var builder = Host.CreateApplicationBuilder(args);

// Only use configuration in appsettings.json
builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile("appsettings.json", false);
builder.Configuration.AddUserSecrets<Program>();
//Disable logging
builder.Logging.ClearProviders();

// Bind configuration section to object
builder.Services.AddOptions<NestedSettings>()
    .Bind(builder.Configuration.GetSection(NestedSettings.Key));

builder.Services.AddOpenAIChatService(builder.Configuration.GetSection(OpenAIOptions.Key));

// builder.Services.AddOptions<OpenAI>()
//     .Bind(builder.Configuration.GetSection(OpenAI.Key))
//     .ValidateDataAnnotations()
//     .ValidateOnStart();

// builder.Services.AddSingleton<IChatCompletionService>(sp =>
// {
//     OpenAI options = sp.GetRequiredService<IOptions<OpenAI>>().Value;
//     return new OpenAIChatCompletionService(options.ChatModelId, options.ApiKey);
// });

// Add semantic kernel Kernel
builder.Services.AddTransient<Kernel>();

// Add a command and optionally configure it.
builder.Services.AddCommand<HelloCommand>("hello", cmd =>
{
    cmd.WithDescription("A command that says hello");
});


// // Add another command and its dependent service

// builder.Services.AddCommand<OtherCommand>("other");
// builder.Services.AddScoped<ISampleService, SampleService>(s => new SampleService("Other Service"));

//
// The standard call save for the commands will be pre-added & configured
//
builder.UseSpectreConsole<HelloCommand>(config =>
{
    // All commands above are passed to config.AddCommand() by this point

    config.SetApplicationName("hello");
    config.UseBasicExceptionHandler();
});

var app = builder.Build();
await app.RunAsync();

#if DEBUG
Console.WriteLine("Press <Enter> to exit.");
Console.ReadLine();
#endif