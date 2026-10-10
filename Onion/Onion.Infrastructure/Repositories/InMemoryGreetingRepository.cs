using System.Collections.Concurrent;
using Onion.Application.Interfaces;
using Onion.Domain.Entities;

namespace Onion.Infrastructure.Repositories;

// In a real app this would persist to a database. For the sample the
// greetings are kept in memory for the lifetime of the process.
public class InMemoryGreetingRepository : IGreetingRepository
{
    private readonly ConcurrentQueue<Greeting> _greetings = new();

    public void Save(Greeting greeting)
    {
        ArgumentNullException.ThrowIfNull(greeting);

        _greetings.Enqueue(greeting);
    }
}
