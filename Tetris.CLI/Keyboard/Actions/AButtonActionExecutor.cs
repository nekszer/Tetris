namespace Tetris.CLI.Keyboard.Actions
{
    public class AButtonActionExecutor : IKeyboardActionExecutor
    {
        public Position[] Execute(IFigurePositionUpdater figurePositionUpdater, IFigure figure, Position[] positions, int verticalSize, int horizontalSize)
        {
            var finalPosition = figurePositionUpdater.Rotate(figure);
            return finalPosition;
        }
    }
}