namespace Onion.Application.Models;

public sealed class CreateGreetingRequest
{
    // Nullable on purpose: the input may be missing and the domain decides
    // whether that is acceptable.
    public string? Name { get; set; }
}
