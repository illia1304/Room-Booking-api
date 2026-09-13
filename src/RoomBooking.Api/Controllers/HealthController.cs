using Microsoft.AspNetCore.Mvc;
using RoomBooking.Api.Contracts;

namespace RoomBooking.Api.Controllers;

[ApiController]
[Route("api/v1/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<HealthResponse>(StatusCodes.Status200OK)]
    public ActionResult<HealthResponse> Get()
    {
        var response = new HealthResponse("Healthy");

        return Ok(response);
    }
}