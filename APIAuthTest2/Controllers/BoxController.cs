using APIAuthTest2.Controllers.RequestBodies;
using APIAuthTest2.Models;
using APIAuthTest2.Services.BoxServices;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIAuthTest2.Controllers;

public class BoxController : AppControllerBase
{
    private readonly IBoxService _boxService;

    public BoxController(IBoxService boxService)
    {
        _boxService = boxService;
    }

    [HttpGet("/box")]
    public IActionResult GetAllBoxes()
    {
        ErrorOr<List<BoxModel>> serviceResponse =
            _boxService.GetAllBoxes();

        if (serviceResponse.IsError)
        {
            return Problem("Could not get boxes.");
        }

        List<BoxModel> boxes = serviceResponse.Value;
        return Ok(boxes);
    }

    [Authorize]
    [HttpPost("/box")]
    public IActionResult AddBox(
        [FromBody] BoxAddBody requestBody
    )
    {
        ErrorOr<BoxModel> boxResult = BoxModel.Create(
            requestBody.Width,
            requestBody.Height,
            requestBody.Depth
        );

        if (boxResult.IsError)
        {
            return Problem("Could not create box.");
        }
        BoxModel box = boxResult.Value;

        ErrorOr<Updated> serviceResponse = _boxService.AddBox(box);

        if (serviceResponse.IsError)
        {
            return Problem("Could not add box.");
        }
        return Ok(box);
    }
}
