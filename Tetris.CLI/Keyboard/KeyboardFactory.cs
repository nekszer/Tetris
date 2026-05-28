using Tetris.CLI.Keyboard.Actions;

namespace Tetris.CLI.Keyboard
{
    public class KeyboardFactory
    {
        private IDictionary<KeyboardAction, IKeyboardActionExecutor> KeyboardActions { get; set; } = new Dictionary<KeyboardAction, IKeyboardActionExecutor>();

        public KeyboardFactory()
        {
            KeyboardActions.Add(KeyboardAction.LeftArrow, new MoveLeftActionExecutor());
            KeyboardActions.Add(KeyboardAction.RightArrow, new MoveRightActionExecutor());
            KeyboardActions.Add(KeyboardAction.DownArrow, new MoveDownActionExecutor());
            KeyboardActions.Add(KeyboardAction.A, new AButtonActionExecutor());

        }

        public IKeyboardActionExecutor Resolve(KeyboardAction action)
        {
            if(KeyboardActions.ContainsKey(action))
                return KeyboardActions[action];
            return null;
        }
    }

    public enum KeyboardAction
    {
        None, LeftArrow, RightArrow, DownArrow, UpArrow, A, B
    }

    public interface IKeyboardActionExecutor
    {
        Position[] Execute(IFigurePositionUpdater figurePositionUpdater, IFigure figure, Position[] positions, int verticalSize, int horizontalSize);
    }
}