public class FigurePositionUpdater : IFigurePositionUpdater
{
    public Position[] Falling(Position[] positions, int ylimit)
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

    public Position[] MoveLeft(Position[] currentPosition, int xLimit, int leftLimit)
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

    public Position[] MoveRight(Position[] currentPosition, int xLimit, int rightLimit)
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
}