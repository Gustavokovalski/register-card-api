using System.Diagnostics.CodeAnalysis;

namespace RegisterCard.Application.Common.Bases;

[ExcludeFromCodeCoverage]
public class BaseError
{
    public string? PropertyMessage { get; set; }
    public string? ErrorMessage { get; set; }
}