using ErrorOr;

using APIAuthTest2.Models;

namespace APIAuthTest2.Services.BoxServices;

public interface IBoxService
{
    ErrorOr<List<BoxModel>> GetAllBoxes();

    ErrorOr<BoxModel> GetBoxById(Guid boxId);

    ErrorOr<Updated> AddBox(BoxModel box);
}
