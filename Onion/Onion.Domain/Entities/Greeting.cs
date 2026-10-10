namespace Onion.Domain.Entities;

public sealed record Greeting(string Message)
{
    // How a greeting is worded is a domain rule, so it lives here and not in
    // the repository or the controller.
    public static Greeting For(Person person)
    {
        ArgumentNullException.ThrowIfNull(person);

        return new Greeting($"Hello, {person.Name}!");
    }
}
