public class Z : IFigure
{
    public Position[] Position { get; set; }
    public bool IsMoving { get; set; }

    public Z(int startPosition)
    {
        Position = new Position[]
        {
            new Position { X = startPosition - 1, Y = 1, Used = true },
            new Position { X = startPosition, Y = 1, Used = true },
            new Position { X = startPosition, Y = 2, Used = true },
            new Position { X = startPosition + 1, Y = 2, Used = true }
        };
        IsMoving = true;
    }
}