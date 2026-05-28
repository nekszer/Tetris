public class Z : IFigure
{
    public Position[] Position { get; set; }
    public bool IsMoving { get; set; }
    public IFigure.Orientation Direction { get; set; }

    public Z(int startPosition)
    {
        Direction = IFigure.Orientation.Horizontal;
        Position = new Position[]
        {
            new Position { X = startPosition - 1, Y = 1, Used = true },
            new Position { X = startPosition, Y = 1, Used = true },
            new Position { X = startPosition, Y = 2, Used = true },
            new Position { X = startPosition + 1, Y = 2, Used = true }
        };
        IsMoving = true;
    }

    public Position[] Rotate()
    {
        if (Direction == IFigure.Orientation.Horizontal)
        {
            Direction = IFigure.Orientation.Vertical;
            var minY = Position.Min(p => p.Y);
            var col = Position.Max(p => p.X) - 1;

            return new Position[]
            {
                new Position { X = col + 1, Y = minY,     Used = true },
                new Position { X = col,     Y = minY + 1, Used = true },
                new Position { X = col + 1, Y = minY + 1, Used = true },
                new Position { X = col,     Y = minY + 2, Used = true }
            };
        }
        else
        {
            Direction = IFigure.Orientation.Horizontal;
            var minY = Position.Min(p => p.Y);
            var col = Position.Min(p => p.X);

            return new Position[]
            {
                new Position { X = col,     Y = minY, Used = true },
                new Position { X = col + 1, Y = minY, Used = true },
                new Position { X = col + 1, Y = minY + 1, Used = true },
                new Position { X = col + 2, Y = minY + 1, Used = true }
            };
        }
    }
}