using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult GetPublicData()
    {
        return Ok(new { Message = "This is public data" });
    }

    [Authorize]
    [HttpGet("private")]
    public IActionResult GetPrivateData()
    {
        return Ok(new { Message = "This is private data, you are authenticated" });
    }
}
