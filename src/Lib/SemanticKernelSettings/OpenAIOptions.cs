using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace semantic_console.Lib.SemanticKernelSettings;

public sealed class OpenAIOptions
{
    public const string Key = "OpenAI";
    private JsonSerializerOptions _jsonOptions = new JsonSerializerOptions() { WriteIndented = true };

    [Required]
    public string ChatModelId { get; set; } = string.Empty;
    [Required]
    public string ApiKey { get; set; } = string.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(_jsonOptions);
    }
}