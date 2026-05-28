using Tetris.CLI.Keyboard;

public partial class GameState
{
    public GameInfo Info { get; set; }
    public IEnumerable<Position> Positions { get; set; } = Enumerable.Empty<Position>();
    public IFigure CurrentFigure { get; set; }
    private IFigureFactory Factory { get; set; }
    private IFigurePositionUpdater FigurePositionUpdater { get; }
    public bool CanFigureFall { get; internal set; }

    public GameState(GameInfo gameInfo, IFigureFactory factory, IFigurePositionUpdater figurePositionUpdater)
    {
        Info = gameInfo ?? throw new ArgumentNullException(nameof(gameInfo));
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));

        var listPositions = new List<Position>();
        for (int i = 0; i < gameInfo.VerticalSize; i++)
            for (int j = 0; j < gameInfo.HorizontalSize; j++)
                listPositions.Add(new Position { X = j, Y = i, Used = false });

        Positions = listPositions;
        CurrentFigure = factory.CreateFigure(gameInfo.HorizontalSize) ?? throw new NullReferenceException("Figura no encontrada");
        FigurePositionUpdater = figurePositionUpdater ?? throw new ArgumentNullException(nameof(figurePositionUpdater));
    }

    public GameState UpdateState()
    {
        var verticalSize = Info.VerticalSize;
        var horizontalSize = Info.HorizontalSize;

        if (!CurrentFigure.IsMoving)
        {
            CurrentFigure = Factory.CreateFigure(horizontalSize);
            if (CurrentFigure == null)
                return CopyGameState();

            var hasBlocked = new ColisionDetector(Positions.Where(p => p.Used).ToArray()).DetectColision(CurrentFigure.Position, verticalSize, horizontalSize);
            if (hasBlocked)
            {
                GameOver = true;
                return CopyGameState();
            }

            return CopyGameState();
        }

        var positionsUsed = Positions.Where(p => p.Used).ToArray();

        Position[]? finalPositions = CanFigureFall ?
            FigurePositionUpdater.Falling(CurrentFigure.Position, positionsUsed, verticalSize) :
            new KeyboardFactory().Resolve(KeyboardInstance.GetAction())?.Execute(FigurePositionUpdater, CurrentFigure, positionsUsed, Info.VerticalSize, Info.HorizontalSize);

        if (finalPositions == null)
            return CopyGameState();

        var hasBlockedByColition = new ColisionDetector(positionsUsed).DetectColision(finalPositions, verticalSize, horizontalSize);
        if (!hasBlockedByColition)
        {
            CurrentFigure.Position = finalPositions;
            return CopyGameState();
        }

        CurrentFigure.IsMoving = false;
        foreach (var item in finalPositions)
        {
            var positionFound = Positions.FirstOrDefault(p => p.Y == item.Y && p.X == item.X);
            if (positionFound == null)
                continue;

            positionFound.Used = true;

            DeleteCompletions(Positions);
        }

        return CopyGameState();
    }

    private GameState CopyGameState()
    {
        return new GameState(Info, Factory, FigurePositionUpdater)
        {
            Positions = Positions,
            KeyboardInstance = KeyboardInstance,
            GameOver = GameOver,
            CurrentFigure = CurrentFigure,
            CanFigureFall = CanFigureFall
        };
    }

    private void DeleteCompletions(IEnumerable<Position> positions)
    {
        var rows = positions.Where(d => d.X != 0).GroupBy(p => p.Y);
        var rowsCompletions = rows.Where(s => s.ToList().All(r => r.Used));
        var data = rowsCompletions.SelectMany(s => s.ToArray()).OrderByDescending(d => d.Y);

        var rowsCompleted = rowsCompletions.Count();
        Info.Lines += rowsCompleted;
        Info.Points += rowsCompleted * (10 * rowsCompleted);

        var positionList = Positions.ToList();
        foreach (var item in data)
        {
            var index = positionList.IndexOf(item);
            var position = positionList.ElementAt(index);
            position.Used = false;
        }

        // TODO: Move columns
    }

    private KeyboardListener KeyboardInstance { get; set; }
    public bool GameOver { get; set; } = false;

    public void SetKeyboardListener(KeyboardListener instance)
    {
        KeyboardInstance = instance;
    }

    public override bool Equals(object? obj)
    {
        var gameState = obj as GameState;
        if (gameState == null)
            return false;


        var figure = gameState.CurrentFigure;
        var currentFigure = CurrentFigure;

        var positions = gameState.Positions;
        var currentPositions = Positions;

        return !figure.Equals(currentFigure) || !positions.SequenceEqual(currentPositions);
    }
}
