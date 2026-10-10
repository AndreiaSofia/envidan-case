using Onion.Domain.Entities;

namespace Onion.Application.Interfaces;

// Repository abstraction used by the application layer. The implementation
// lives in the infrastructure layer and is chosen at startup through DI.
public interface IGreetingRepository
{
    void Save(Greeting greeting);
}
