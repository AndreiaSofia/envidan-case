using Onion.Domain.Entities;
using Onion.Application.Models;

namespace Onion.Application.Interfaces;

public interface IGreetingService
{
    Greeting CreateGreeting(CreateGreetingRequest request);
}
