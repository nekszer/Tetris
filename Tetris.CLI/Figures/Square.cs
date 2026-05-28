public class Square : IFigure
{
    private int startPosition;

    public Square(int startPosition)
    {
        this.startPosition = startPosition;
        Position = new Position[]
        {
            new Position { X = startPosition, Y = 1, Used = true, },
            new Position { X = startPosition, Y = 2, Used = true, },
            new Position { X = startPosition + 1, Y = 1, Used = true, },
            new Position { X = startPosition + 1, Y = 2, Used = true, }
        };
        IsMoving = true;
    }

    public Position[] Position { get; set; }
    public bool IsMoving { get; set; }
    public IFigure.Orientation Direction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    
    public Position[] Rotate()
    {
        return Position;
    }
}