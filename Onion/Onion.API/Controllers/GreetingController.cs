using Microsoft.AspNetCore.Mvc;
using Onion.API.Models;
using Onion.Application.Interfaces;
using Onion.Application.Models;
using Onion.Domain.Entities;

namespace Onion.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GreetingController : ControllerBase
{
    private readonly IGreetingService _service;

    public GreetingController(IGreetingService service)
    {
        _service = service;
    }

    [HttpPost]
    public ActionResult<GreetingResponse> Post([FromBody] GreetingRequest request)
    {
        var appRequest = new CreateGreetingRequest { Name = request.Name };
        var greeting = _service.CreateGreeting(appRequest);
        return Ok(new GreetingResponse(greeting.Message));
    }
}
