public class Rectangle
{
    public string Id { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public double HorizontalCordinate { get; set; }
    public double VerticalCordinate { get; set; }

    public Rectangle(string id, int width, int height, double horizontalCordinate, double verticalCordinate)
    {
        this.Id = id;
        this.Width = width;
        this.Height = height;
        this.HorizontalCordinate = horizontalCordinate;
        this.VerticalCordinate = verticalCordinate;
    }

    public bool DoesIntersect(Rectangle rectangle)
    {
        return !(this.VerticalCordinate > rectangle.VerticalCordinate + rectangle.Width) &&
               !(this.VerticalCordinate + rectangle.Width < rectangle.VerticalCordinate) &&
               !(this.HorizontalCordinate > rectangle.HorizontalCordinate + rectangle.Height) &&
               !(this.HorizontalCordinate + this.Height < rectangle.HorizontalCordinate);
    }
}