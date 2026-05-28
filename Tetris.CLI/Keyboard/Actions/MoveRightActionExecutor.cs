namespace Tetris.CLI.Keyboard.Actions
{
    public class MoveRightActionExecutor : IKeyboardActionExecutor
    {
        public Position[] Execute(IFigurePositionUpdater figurePositionUpdater, IFigure figure, Position[] positions, int verticalSize, int horizontalSize)
        {
            return figurePositionUpdater.MoveRight(figure.Position, positions.Where(p => p.Used).ToArray(), verticalSize, horizontalSize);
        }
    }
}