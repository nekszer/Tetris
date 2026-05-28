namespace Tetris.CLI.Keyboard.Actions
{
    public class MoveLeftActionExecutor : IKeyboardActionExecutor
    {
        public Position[] Execute(IFigurePositionUpdater figurePositionUpdater, IFigure figure, List<Position> positions, int verticalSize, int horizontalSize)
        {
            return figurePositionUpdater.MoveLeft(figure.Position, positions.Where(p => p.Used).ToArray(), verticalSize, 0);
        }
    }

    public class MoveRightActionExecutor : IKeyboardActionExecutor
    {
        public Position[] Execute(IFigurePositionUpdater figurePositionUpdater, IFigure figure, List<Position> positions, int verticalSize, int horizontalSize)
        {
            return figurePositionUpdater.MoveRight(figure.Position, positions.Where(p => p.Used).ToArray(), verticalSize, horizontalSize);
        }
    }

    public class MoveDownActionExecutor : IKeyboardActionExecutor
    {
        public Position[] Execute(IFigurePositionUpdater figurePositionUpdater, IFigure figure, List<Position> positions, int verticalSize, int horizontalSize)
        {
            return figurePositionUpdater.MoveDown(figure.Position, positions.Where(p => p.Used).ToArray(), verticalSize, verticalSize - 1);
        }
    }

    public class AButtonActionExecutor : IKeyboardActionExecutor
    {
        public Position[] Execute(IFigurePositionUpdater figurePositionUpdater, IFigure figure, List<Position> positions, int verticalSize, int horizontalSize)
        {
            var finalPosition = figurePositionUpdater.Rotate(figure);
            return finalPosition;
        }
    }
}