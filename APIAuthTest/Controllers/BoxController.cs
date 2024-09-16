using Microsoft.AspNetCore.Mvc;

using ErrorOr;

using APIAuthTest.Services.BoxServices;
using APIAuthTest.Models;
using APIAuthTest.Controllers.RequestBodies;

namespace APIAuthTest.Controllers;

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

    [HttpGet("/box/{id:guid}")]
    public IActionResult GetBoxById(Guid id)
    {
        ErrorOr<BoxModel> serviceResponse = _boxService.GetBoxById(id);

        if (serviceResponse.IsError)
        {
            return Problem("Could not find box.");
        }

        BoxModel box = serviceResponse.Value;
        return Ok(box);
    }

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
