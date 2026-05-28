public class Line : IFigure
{
    private int startPosition;

    public Line(int startPosition)
    {
        this.startPosition = startPosition;

        IsMoving = true;


        var orientationList = new IFigure.Orientation[] { IFigure.Orientation.Horizontal, IFigure.Orientation.Vertical };
        Random random = new Random();
        Direction = orientationList[random.Next(orientationList.Length)];

        if(Direction == IFigure.Orientation.Vertical)
        {
            Direction = IFigure.Orientation.Vertical;
            Position = new Position[]
            {
                new Position { X = startPosition, Y = -3, Used = true },
                new Position { X = startPosition, Y = -2, Used = true },
                new Position { X = startPosition, Y = -1, Used = true },
                new Position { X = startPosition, Y = 0, Used = true },
                new Position { X = startPosition, Y = 1, Used = true }
            };
        }
        else
        {
            Direction = IFigure.Orientation.Horizontal;
            Position = new Position[]
            {
                new Position { X = startPosition - 2, Y = 1, Used = true },
                new Position { X = startPosition - 1, Y = 1, Used = true },
                new Position { X = startPosition, Y = 1, Used = true },
                new Position { X = startPosition + 1, Y = 1, Used = true },
                new Position { X = startPosition + 2, Y = 1, Used = true }
            };
        }
    }

    public Position[] Position { get; set; }
    public bool IsMoving { get; set; }
    public IFigure.Orientation Direction { get; set; }

    public Position[] Rotate()
    {
        if(Direction == IFigure.Orientation.Vertical)
        {
            Direction = IFigure.Orientation.Horizontal;

            var maxY   = Position.Max(p => p.Y);
            var minY   = Position.Min(p => p.Y);
            var centerY = (maxY + minY) / 2;
            var x      = Position[0].X; // columna fija de la pieza vertical

            return new Position[]
            {
                new Position { X = x - 2, Y = centerY, Used = true },
                new Position { X = x - 1, Y = centerY, Used = true },
                new Position { X = x,     Y = centerY, Used = true },
                new Position { X = x + 1, Y = centerY, Used = true },
                new Position { X = x + 2, Y = centerY, Used = true }
            };
        }
        else
        {
            Direction = IFigure.Orientation.Vertical;

            var maxX    = Position.Max(p => p.X);
            var minX    = Position.Min(p => p.X);
            var centerX = (maxX + minX) / 2; // columna central de la pieza horizontal
            var y       = Position[0].Y; // fila fija de la pieza horizontal

            return new Position[]
            {
                new Position { X = centerX, Y = y - 2, Used = true },
                new Position { X = centerX, Y = y - 1, Used = true },
                new Position { X = centerX, Y = y,     Used = true },
                new Position { X = centerX, Y = y + 1, Used = true },
                new Position { X = centerX, Y = y + 2, Used = true }
            };
        }
    }
}