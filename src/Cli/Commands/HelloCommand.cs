using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using semantic_console.Lib.SemanticKernelSettings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace semantic_console.Cli.Commands;

public class HelloCommand(IAnsiConsole ansiConsole, IOptions<OpenAIOptions> openAIOptions, Kernel kernel) : AsyncCommand<HelloCommand.Settings>
{
    private readonly IAnsiConsole _console = ansiConsole;
    private readonly Kernel _kernel = kernel;
    private readonly OpenAIOptions _openAI = openAIOptions.Value;
    private ChatHistory _history = [];

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

        _console.Markup("[blue]User > [/]");
        string? userInput;
        while ((userInput = Console.ReadLine()) != null)
        {
            _history.AddUserMessage(userInput);

            OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            };

            var result = await chatCompletionService.GetChatMessageContentAsync(_history, openAIPromptExecutionSettings, _kernel);

            _console.MarkupLine("[green]Assistant > [/]" + result);

            _history.AddMessage(result.Role, result.InnerContent?.ToString() ?? string.Empty);

            _console.Markup("[blue]User > [/]");
        }

        _console.WriteLine(_openAI.ToString());

        return await Task.FromResult(0);
    }

    public class Settings : CommandSettings
    {
    }
}
