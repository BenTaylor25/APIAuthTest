using ErrorOr;

namespace APIAuthTest.Models;

public class BoxModel
{
    public Guid Id { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }

    private BoxModel(
        Guid id,
        double width,
        double height,
        double depth
    )
    {
        Id = id;
        Width = width;
        Height = height;
        Depth = depth;
    }

    public static ErrorOr<BoxModel> Create(
        double width,
        double height,
        double depth
    )
    {
        bool isValid =
            width > 0 &&
            height > 0 &&
            depth > 0;

        if (!isValid)
        {
            return Error.Validation();
        }

        Guid id = Guid.NewGuid();
        return new BoxModel(id, width, height, depth);
    }
}
