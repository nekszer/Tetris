using Tetris.CLI.Keyboard;

public partial class GameSate
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

    public GameUpdateStateResult UpdateState()
    {
        if (CurrentFigure == null)
            return new GameUpdateStateResult { IsValid = false, Points = 0, State = "Figura no encontrada" };

        var verticalSize = Info.VerticalSize;
        var horizontalSize = Info.HorizontalSize;

        if (!CurrentFigure.IsMoving)
        {
            CurrentFigure = Factory.CreateFigure(horizontalSize);
            var hasBlocked = new ColisionDetector(Positions.Where(p => p.Used).ToArray()).DetectColision(CurrentFigure.Position, verticalSize, horizontalSize);
            if (hasBlocked)
                return new GameUpdateStateResult { IsValid = false, Points = Info.Points, State = "Fin del juego" };

            return new GameUpdateStateResult { IsValid = true, Points = Info.Points, State = "Nueva figura en curso" };
        }

        Position[] finalPositions = null;
        if (CanFigureFall)
        {
            finalPositions = FigurePositionUpdater.Falling(CurrentFigure.Position, Positions.Where(d => d.Used).ToArray(), verticalSize);
        }
        else
        {
            var keyboardActionExecutor = new KeyboardFactory().Resolve(KeyboardInstance.GetAction());
            if(keyboardActionExecutor != null)
                finalPositions = keyboardActionExecutor.Execute(FigurePositionUpdater, CurrentFigure, Positions, Info.VerticalSize, Info.HorizontalSize);
        }

        if (finalPositions == null)
            return new GameUpdateStateResult { IsValid = true, Points = Info.Points, State = "Sin cambios" };


        var hasBlockedByColition = new ColisionDetector(Positions.Where(p => p.Used).ToArray()).DetectColision(finalPositions, verticalSize, horizontalSize);
        if (!hasBlockedByColition)
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

    private KeyboardListener KeyboardInstance { get; set; }
    public void SetKeyboardListener(KeyboardListener instance)
    {
        KeyboardInstance = instance;
    }
}
