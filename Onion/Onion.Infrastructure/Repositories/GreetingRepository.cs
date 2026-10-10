using Onion.Application.Interfaces;
using Onion.Domain.Entities;

namespace Onion.Infrastructure.Repositories;

public class GreetingRepository : IGreetingRepository
{
    public Greeting SaveGreeting(Person person)
    {
        // In a real app this would persist to a database. For the sample we
        // simply format and return the domain Greeting entity.
        var message = $"Hello, {person.Name}!";
        return new Greeting(message);
    }
}
