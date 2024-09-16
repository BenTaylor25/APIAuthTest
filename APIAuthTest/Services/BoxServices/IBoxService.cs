using ErrorOr;

using APIAuthTest.Models;

namespace APIAuthTest.Services.BoxServices;

public interface IBoxService
{
    ErrorOr<List<BoxModel>> GetAllBoxes();

    ErrorOr<BoxModel> GetBoxById(Guid boxId);

    ErrorOr<Updated> AddBox(BoxModel box);
}
