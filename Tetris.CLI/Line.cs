public class Line : IFigure
{
    private int startPosition;

    public Line(int startPosition)
    {
        this.startPosition = startPosition;

        IsMoving = true;
        Position = new Position[]
        {
                    new Position { X = startPosition - 2, Y = 1, Used = true },
                    new Position { X = startPosition - 1, Y = 1, Used = true },
                    new Position { X = startPosition, Y = 1, Used = true },
                    new Position { X = startPosition + 1, Y = 1, Used = true }
        };
    }

    public Position[] Position { get; set; }
    public bool IsMoving { get; set; }
}