public partial class Program
{

    public static void Main(string[] args)
    {
        int verticalSize = 24;
        int horizontalSize = 20;

        var gameInfo = new GameInfo(verticalSize, horizontalSize);
        var gameState = new GameSate(gameInfo, new FigureFactory(), new FigurePositionUpdater());

        byte iterations = 0;
        byte maxValue = byte.MaxValue;
        while (true)
        {
            ConsoleKeyInfo pressKey = new ConsoleKeyInfo();
            if (Console.KeyAvailable)
                pressKey = Console.ReadKey(true);

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
                            Console.Write(gameState.CheckPosition(j, i) ? "O" : " ");
                }
            }

            gameState.CanFigureFall = iterations == 25;
            if (gameState.CanFigureFall)
                iterations = 0;
            else
                gameState.LastKeyPress(pressKey);

            var newState = gameState.UpdateState();
            if (!newState.IsValid)
            {
                Console.Write(newState.State);
                break;
            }

            Thread.Sleep(1);
            LimpiarConsolaForzado();

            iterations++;
        }
    }

    /// <summary>
    /// Limpia la consola de forma segura y forzada.
    /// </summary>
    static void LimpiarConsolaForzado()
    {
        try
        {
            // Intentar limpieza estándar
            Console.Clear();
        }
        catch
        {
            // Si falla, limpiar manualmente
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