using Onion.Application.Models;

namespace Onion.Application.Interfaces;

public interface IGreetingService
{
    GreetingResult CreateGreeting(CreateGreetingRequest request);
}
