using Tetris.CLI.Keyboard;

public partial class Program
{
    public static void Main(string[] args)
    {
        int verticalSize = 18;
        int horizontalSize = 12;

        var gameInfo = new GameInfo(verticalSize, horizontalSize);
        var gameState = new GameState(gameInfo, new FigureFactory(), new FigurePositionUpdater());

        var keyboardListener = KeyboardListener.Instance;
        gameState.SetKeyboardListener(keyboardListener);

        byte iterations = 0;

        GameGraphics.DrawTable(gameState);

        while (true)
        {
            keyboardListener.Listen();

            gameState.CanFigureFall = iterations == 25;

            if (gameState.CanFigureFall)
                iterations = 0;
            
            var newState = gameState.UpdateState();
            Thread.Sleep(10);
            iterations++;

            if (newState.GameOver)
            {
                Console.WriteLine("Juego terminado!!");
                Console.WriteLine("Tu puntuación: " + newState.Info.Points);
                break;
            }

            if (newState.Equals(gameState))
                continue;
            
            gameState = newState;
            GameGraphics.DrawFigures(gameState);
        }
    }
}