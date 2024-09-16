using ErrorOr;

using APIAuthTest.Models;

namespace APIAuthTest.Services.BoxServices;

public class BoxService : IBoxService
{
    private readonly List<BoxModel> _boxes;

    public BoxService()
    {
        _boxes = new List<BoxModel>();

        for (int i = 0; i < 3; i++)
        {
            ErrorOr<BoxModel> box = BoxModel.Create(
                i * 2 + 1,
                i * 3 + 2,
                i * 4 + 3
            );

            if (!box.IsError)
            {
                _boxes.Add(box.Value);
            }
        }
    }

    public ErrorOr<List<BoxModel>> GetAllBoxes()
    {
        return _boxes;
    }

    public ErrorOr<BoxModel> GetBoxById(Guid boxId)
    {
        foreach (BoxModel box in _boxes)
        {
            if (box.Id == boxId)
            {
                return box;
            }
        }

        return Error.NotFound();
    }

    public ErrorOr<Updated> AddBox(BoxModel box)
    {
        _boxes.Add(box);
        return Result.Updated;
    }
}
