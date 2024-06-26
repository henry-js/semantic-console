using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using semantic_console.Lib.Chat;
using semantic_console.Lib.SemanticKernelSettings;

namespace semantic_console.Lib.Extensions;

public static class SemanticKernelChatExtensions
{
    public static IServiceCollection AddOpenAIChatService(this IServiceCollection services, IConfiguration configSectionPath)
    {
        services.Configure<OpenAIOptions>(configSectionPath);
        services.AddSingleton<IChatCompletionService>(sp =>
        {
            OpenAIOptions options = sp.GetRequiredService<IOptions<OpenAIOptions>>().Value;
            return new OpenAIChatCompletionService(options.ChatModelId, options.ApiKey);
        });

        services.AddSingleton<ChatApp>();

        return services;
    }
}