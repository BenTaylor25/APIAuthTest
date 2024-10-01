namespace APIAuthTest2.Controllers.RequestBodies;

public class BoxAddBody
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }

    public BoxAddBody(
        double width,
        double height,
        double depth
    )
    {
        Width = width;
        Height = height;
        Depth = depth;
    }
}
