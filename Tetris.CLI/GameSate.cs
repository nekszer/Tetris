public class GameSate
{
    public GameInfo Info { get; set; }
    public List<Position> Positions { get; set; } = [];
    public IFigure CurrentFigure { get; set; }
    private IFigureFactory Factory { get; set; }
    private IFigurePositionUpdater FigurePositionUpdater { get; }
    public bool CanFigureFall { get; internal set; }

    public GameSate(GameInfo gameInfo, IFigureFactory factory, IFigurePositionUpdater figurePositionUpdater)
    {
        Info = gameInfo;
        Factory = factory;

        for (int i = 0; i < gameInfo.VerticalSize; i++)
            for (int j = 0; j < gameInfo.HorizontalSize; j++)
                Positions.Add(new Position { X = j, Y = i, Used = false });

        CurrentFigure = factory.CreateFigure(gameInfo.HorizontalSize);
        FigurePositionUpdater = figurePositionUpdater;
    }

    public bool CheckPosition(int j, int i)
    {
        if (CurrentFigure == null)
            return false;

        // Buscamos la posicion en la figura actual
        foreach (var position in CurrentFigure.Position)
            if (position.X == j && position.Y == i)
                return true;

        // Buscamos la posicion en las figuras que han caido
        var stateSaved = Positions.Where(p => p.Used).FirstOrDefault(p => p.Y == i && p.X == j);
        if (stateSaved != null && stateSaved.X == j && stateSaved.Y == i)
            return true;

        return false;
    }

    public class GameUpdateStateResult
    {
        public long Points { get; set; }
        public bool IsValid { get; set; }
        public string State { get; set; }
    }

    public GameUpdateStateResult UpdateState()
    {
        if (CurrentFigure == null)
            return new GameUpdateStateResult { IsValid = false, Points = 0, State = "Figura no encontrada" };

        var verticalSize = Info.VerticalSize;
        var horizontalSize = Info.HorizontalSize;

        if (!CurrentFigure.IsMoving)
        {
            CurrentFigure = Factory.CreateFigure(horizontalSize);
            var y = GetMaxVerticalPosition(verticalSize);

            if (CurrentFigure.Position.Any(p => p.Y == y))
                return new GameUpdateStateResult { IsValid = false, Points = Info.Points, State = "Fin del juego" };

            return new GameUpdateStateResult { IsValid = true, Points = Info.Points, State = "Nueva figura en curso" };
        }

        Position[] finalPositions = null;
        if (CanFigureFall)
        {
            finalPositions = FigurePositionUpdater.Falling(CurrentFigure.Position, verticalSize);
        }
        else
        {
            if (LastKeyPressed.Key.ToString() == "RightArrow")
                finalPositions = FigurePositionUpdater.MoveRight(CurrentFigure.Position, verticalSize, horizontalSize);

            if (LastKeyPressed.Key.ToString() == "LeftArrow")
                finalPositions = FigurePositionUpdater.MoveLeft(CurrentFigure.Position, verticalSize, 0);
        }

        if (finalPositions == null)
            return new GameUpdateStateResult { IsValid = true, Points = Info.Points, State = "Sin cambios" };

        if (!DetectBlockPosition(finalPositions, verticalSize))
        {
            CurrentFigure.Position = finalPositions;
            return new GameUpdateStateResult { IsValid = true, Points = Info.Points, State = "Actualización de figura" };
        }

        CurrentFigure.IsMoving = false;
        foreach (var item in finalPositions)
        {
            var positionFound = Positions.FirstOrDefault(p => p.Y == item.Y && p.X == item.X);
            if (positionFound == null)
                continue;

            positionFound.Used = true;
        }

        return new GameUpdateStateResult { IsValid = true, Points = Info.Points, State = "Colisión" };
    }

    private bool DetectBlockPosition(Position[] finalPositions, int verticalSize)
    {
        var y = GetMaxVerticalPosition(verticalSize);
        foreach (var position in finalPositions.OrderBy(p => p.Y))
        {
            if (position.Y == y)
                return true;
        }

        return false;
    }

    public int GetMaxVerticalPosition(int verticalSize)
    {
        try
        {
            var yMax = Positions.Where(p => p.Used).Min(p => p.Y);
            return yMax - 1;
        }
        catch
        {
            return verticalSize - 1;
        }
    }

    public int GetMaxHorizontalPosition(int horizontalSize)
    {
        try
        {
            return Positions.Where(p => p.Used).Max(p => p.X);
        }
        catch { return horizontalSize - 1; }
    }

    private ConsoleKeyInfo LastKeyPressed { get; set; }
    public void LastKeyPress(ConsoleKeyInfo pressKey)
    {
        LastKeyPressed = pressKey;
    }
}