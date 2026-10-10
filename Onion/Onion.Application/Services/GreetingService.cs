using Onion.Application.Interfaces;
using Onion.Domain.Entities;
using Onion.Application.Models;

namespace Onion.Application.Services;

public class GreetingService : IGreetingService
{
    private readonly IGreetingRepository _repository;

    public GreetingService(IGreetingRepository repository)
    {
        _repository = repository;
    }

    public Greeting CreateGreeting(CreateGreetingRequest request)
    {
        // Map application request -> domain object. Domain validation is
        // enforced by the Person constructor.
        var person = new Person(request.Name);
        var greeting = _repository.SaveGreeting(person);
        return greeting;
    }
}
