public class Rectangle
{
    public Rectangle(double x1, double y1, double width, double height)
    {
        this.X1 = x1;
        this.Y1 = y1;
        this.X2 = x1 + width;
        this.Y2 = y1 + height;
    }

    public double Y1 { get; set; }

    public double X1 { get; set; }

    public double Y2 { get; set; }

    public double X2 { get; set; }

    public double Width { get { return this.X2 - this.X1; } }

    public double Height { get { return this.Y2 - this.Y1; } }

    public bool Intersects(Rectangle other)
    {
        return this.X1 <= other.X2 &&
               other.X1 <= this.X2 &&
               this.Y1 <= other.Y2 &&
               other.Y1 <= this.Y2;
    }

    public bool IsInside(Point2D other)
    {
        return this.X2 >= other.X &&
               this.X1 <= other.X &&
               this.Y1 <= other.Y &&
               this.Y2 >= other.Y;
//        (this.getLocation().getX() < p.getX() && this.getLocation().getY() < p.getY() &&
//         this.getLocation().getX() + this.getWidth() > p.getX() &&
//         this.getLocation().getY() + this.getHeight() > p.getY());
    }

    public override string ToString()
    {
        return $"({this.X1}, {this.Y1}) .. ({this.X2}, {this.Y2})";
    }
}
