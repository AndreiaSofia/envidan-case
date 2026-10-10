using Onion.Application.Interfaces;
using Onion.Application.Models;
using Onion.Domain.Entities;

namespace Onion.Application.Services;

public class GreetingService : IGreetingService
{
    private readonly IGreetingRepository _repository;

    public GreetingService(IGreetingRepository repository)
    {
        _repository = repository;
    }

    public GreetingResult CreateGreeting(CreateGreetingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Map application request -> domain objects. Domain validation is
        // enforced by the Person constructor.
        var person = new Person(request.Name);
        var greeting = Greeting.For(person);

        _repository.Save(greeting);

        return new GreetingResult(greeting.Message);
    }
}
