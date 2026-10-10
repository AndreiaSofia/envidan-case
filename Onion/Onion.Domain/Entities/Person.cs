namespace Onion.Domain.Entities;

public sealed class Person
{
    public string Name { get; }

    public Person(string name)
    {
        // Domain-level validation: the Person cannot be created with an empty name.
        if (string.IsNullOrWhiteSpace(name))
            throw new Onion.Domain.Exceptions.DomainException("name is required");

        Name = name;
    }
}
