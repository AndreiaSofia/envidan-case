using Onion.Domain.Exceptions;

namespace Onion.Domain.Entities;

public sealed class Person
{
    public string Name { get; }

    public Person(string? name)
    {
        // Domain-level validation: a Person cannot be created without a name.
        // This is the only place that rule lives; the API just maps the exception.
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("name is required");
        }

        Name = name.Trim();
    }
}
