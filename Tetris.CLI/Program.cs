using Tetris.CLI.Keyboard;

public partial class Program
{

    public static void Main(string[] args)
    {
        int verticalSize = 36;
        int horizontalSize = 30;

        var gameInfo = new GameInfo(verticalSize, horizontalSize);
        var gameState = new GameSate(gameInfo, new FigureFactory(), new FigurePositionUpdater());
        var keyboardListener = KeyboardListener.Instance;
        gameState.SetKeyboardListener(keyboardListener);

        byte iterations = 0;
        byte maxValue = byte.MaxValue;
        while (true)
        {
            keyboardListener.Listen();

            for (int i = 0; i <= verticalSize; i++)
            {
                Console.WriteLine();

                for (int j = 0; j <= horizontalSize; j++)
                {
                    if (i == 0 || i == verticalSize)
                        Console.Write("#");
                    else
                        if (j == 0 || j == horizontalSize)
                            Console.Write("#");
                        else
                            Console.Write(gameState.CheckPosition(j, i) ? "█" : " ");
                }
            }

            gameState.CanFigureFall = iterations == 25;
            if (gameState.CanFigureFall)
                iterations = 0;

            var newState = gameState.UpdateState();
            if (!newState.IsValid)
            {
                Console.Write(newState.State);
                break;
            }

            Thread.Sleep(16);
            LimpiarConsolaForzado();

            iterations++;
        }
    }

    static void LimpiarConsolaForzado()
    {
        try
        {
            Console.Clear();
        }
        catch
        {
            int ancho = Console.WindowWidth;
            int alto = Console.WindowHeight;

            for (int i = 0; i < alto; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', ancho));
            }
            Console.SetCursorPosition(0, 0);
        }
    }
}