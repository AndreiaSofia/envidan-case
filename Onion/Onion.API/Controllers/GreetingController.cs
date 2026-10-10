using Microsoft.AspNetCore.Mvc;
using Onion.Application.Interfaces;
using Onion.Application.Models;

namespace Onion.API.Controllers;

[ApiController]
public class GreetingController : ControllerBase
{
    private readonly IGreetingService _service;

    public GreetingController(IGreetingService service)
    {
        _service = service;
    }

    // Same contract as the original minimal API: GET /hello?name=Ana -> "Hello, Ana!"
    [HttpGet("/hello")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK, "text/plain")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult Get([FromQuery] string? name)
    {
        var result = _service.CreateGreeting(new CreateGreetingRequest { Name = name });

        return Content(result.Message, "text/plain");
    }
}
