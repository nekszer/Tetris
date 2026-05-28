public class FigurePositionUpdater : IFigurePositionUpdater
{
    public Position[] Falling(Position[] positions, Position[] usedPositions, int ylimit)
    {
        foreach (var item in positions.OrderBy(p => p.Y))
        {
            var next = item.Y + 1;
            if (next < ylimit)
                item.Y = next;
            else
                break;
        }

        return positions;
    }

    public Position[] MoveLeft(Position[] currentPosition, Position[] usedPositions, int yLimit, int leftLimit)
    {
        foreach (var item in currentPosition.OrderBy(p => p.X))
        {
            var next = item.X - 1;
            if (next > leftLimit)
                item.X = next;
            else
                break;
        }

        return currentPosition;
    }

    public Position[] MoveRight(Position[] currentPosition, Position[] usedPositions, int xLimit, int rightLimit)
    {
        foreach (var item in currentPosition.OrderByDescending(p => p.X))
        {
            var next = item.X + 1;
            if (next < rightLimit)
                item.X = next;
            else
                break;
        }

        return currentPosition;
    }

    public Position[] MoveDown(Position[] currentPosition, Position[] usedPositions, int yLimit, int downLimit)
    {
        return Falling(currentPosition, usedPositions, yLimit);
    }

    public Position[] Rotate(IFigure figure)
    {
        return figure.Rotate();
    }
}