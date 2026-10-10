using Onion.Domain.Entities;

namespace Onion.Application.Interfaces;

// Repository abstraction used by the application layer.
public interface IGreetingRepository
{
    Greeting SaveGreeting(Person person);
}
