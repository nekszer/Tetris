public class GameGraphics
{
    public static void DrawTable(GameState gameState)
    {
        var height = gameState.Info.VerticalSize;
        var width = gameState.Info.HorizontalSize;

        Console.SetCursorPosition(0, 0);
        Console.Write("#" + new string('#', width * 2 + 1) + "#");

        for (int row = 0; row <= height; row++)
        {
            Console.SetCursorPosition(0, row);
            Console.Write("#");

            Console.SetCursorPosition(width * 2 + 3, row);
            Console.Write("#");
        }

        Console.SetCursorPosition(0, height + 1);
        Console.Write("#" + new string('#', width * 2 + 2) + "#");

        Console.SetCursorPosition(width * 2 + 5, 2);
        Console.WriteLine($"Puntuación: {gameState.Info.Points}");

        Console.SetCursorPosition(width * 2 + 5, 4);
        Console.WriteLine($"Lineas: {gameState.Info.Lines}");
    }

    private static List<Position> FigurePoints { get; set; }

    private static long Points { get; set; }
    private static long Lines { get; set; }

    public static void DrawFigures(GameState gameState)
    {
        if (FigurePoints != null)
        {
            foreach (var bloque in FigurePoints)
            {
                Console.SetCursorPosition(bloque.X * 2 + 1, bloque.Y + 1);
                Console.Write("  ");
            }
        }

        var posicionActual = new List<Position>();
        foreach (var bloque in gameState.CurrentFigure.Position)
        {
            int x = bloque.X;
            int y = bloque.Y;
            Console.SetCursorPosition(x * 2 + 1, y + 1);
            Console.Write("[]");
            posicionActual.Add(new Position(x, y));
        }

        foreach (var bloque in gameState.Positions.Where(d => d.Used))
        {
            int x = bloque.X;
            int y = bloque.Y;
            Console.SetCursorPosition(x * 2 + 1, y + 1);
            Console.Write("[]");
            posicionActual.Add(new Position(x, y));
        }

        FigurePoints = posicionActual;

        if (Points != gameState.Info.Points)
        {
            Console.SetCursorPosition(gameState.Info.HorizontalSize * 2 + 5, 2);
            Console.Write($"Puntuación: {gameState.Info.Points.ToString().PadLeft(8)}");
            Points = gameState.Info.Points;
        }

        // Actualizar líneas solo si cambió
        if (Lines != gameState.Info.Lines)
        {
            Console.SetCursorPosition(gameState.Info.HorizontalSize * 2 + 5, 4);
            Console.Write($"Lineas: {gameState.Info.Lines.ToString().PadLeft(8)}");
            Lines = gameState.Info.Lines;
        }
    }
}
