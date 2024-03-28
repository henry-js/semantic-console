using System.ComponentModel.DataAnnotations;

namespace semantic_console.Lib.SemanticKernelSettings;

public sealed class OpenAI
{
    public const string Key = "OpenAI";

    [Required]
    public string ChatModelId { get; set; } = string.Empty;
    [Required]
    public string ApiKey { get; set; } = string.Empty;
}