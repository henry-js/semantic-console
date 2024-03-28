using System.Text.Json;
using Humanizer;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using semantic_console.Lib.SemanticKernelSettings;
using semantic_console.Lib.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace semantic_console.Cli.Commands;

public class HelloCommand(IAnsiConsole ansiConsole, IOptions<OpenAI> openAIOptions, Kernel kernel) : AsyncCommand<HelloCommand.Settings>
{
    private readonly IAnsiConsole _console = ansiConsole;
    private readonly Kernel _kernel = kernel;
    private readonly OpenAI _openAI = openAIOptions.Value;
    private ChatHistory _history = [];

    public override async Task<int> ExecuteAsync(CommandContext context, HelloCommand.Settings settings)
    {
        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();

        _console.Write("User > ");
        string? userInput;
        while ((userInput = Console.ReadLine()) != null)
        {
            _history.AddUserMessage(userInput);

            OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            };

            var result = await chatCompletionService.GetChatMessageContentAsync(_history, openAIPromptExecutionSettings, _kernel);

            _console.WriteLine("[green]Assistant > [/]" + result);

            _history.AddMessage(result.Role, result.InnerContent?.ToString() ?? string.Empty);

            _console.Write("[blue]User > [/]");
        }
        _console.MarkupLine("[green]Hello[/] [magenta]there![/]");

        _console.MarkupLine($"In appsettings.json value of NestedSettings:");
        _console.WriteLine(JsonSerializer.Serialize(_openAI, new JsonSerializerOptions { WriteIndented = true }));

        return await Task.FromResult(0);
    }

    public class Settings : CommandSettings
    {
    }
}

public class OtherCommand(IAnsiConsole ansiConsole, ISampleService service) : AsyncCommand
{
    private readonly IAnsiConsole _console = ansiConsole;
    private readonly ISampleService _service = service;

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        _console.MarkupLine("[red]I am the other command[/] [blue]that has no options or flags.[/]");
        _console.MarkupLine("[yellow]But I do use the SampleService[/]");

        _service.DoWork();
        return await Task.FromResult(0);
    }
}