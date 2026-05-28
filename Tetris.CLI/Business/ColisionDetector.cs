public class ColisionDetector
{
    private Position[] CurrentPositions { get; }

    public ColisionDetector(Position[] currentPositions)
    {
        CurrentPositions = currentPositions;
    }

    public bool DetectColision(Position[] finalPositions, int verticalSize, int horizontalSize)
    {
        if (finalPositions == null)
            return false;

        foreach (var position in finalPositions)
        {
            if (CurrentPositions.Any(c => c.X == position.X && c.Y == position.Y))
                return true;
            else
            {
                var y = GetMaxVerticalPosition(verticalSize, position.X);
                if (position.Y == y)
                    return true;
            }
        }
        return false;
    }

    public int GetMaxVerticalPosition(int verticalSize, int xPosition)
    {
        try
        {
            var yMax = CurrentPositions.Where(p => p.X == xPosition).Min(p => p.Y);
            return yMax - 1;
        }
        catch
        {
            return verticalSize - 1;
        }
    }
}