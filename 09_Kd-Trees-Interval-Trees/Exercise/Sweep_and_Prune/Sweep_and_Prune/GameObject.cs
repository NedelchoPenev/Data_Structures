class GameObject
{
    private const int Width = 10;
    private const int Height = 10;

    public GameObject(string name, int x1, int y1)
    {
        this.Name = name;
        this.X1 = x1;
        this.Y1 = y1;
    }

    public int Y1 { get; set; }

    public int X1 { get; set; }

    public string Name { get; set; }

    public bool Intersects(GameObject other)
    {
        return this.X1 <= other.X2 &&
               other.X1 <= this.X2 &&
               this.Y1 <= other.Y2 &&
               other.Y1 <= this.Y2;
    }

    public int Y2 => this.Y1 + Height;

    public int X2 => this.X1 + Width;
}