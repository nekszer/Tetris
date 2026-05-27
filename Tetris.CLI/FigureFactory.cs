public class FigureFactory : IFigureFactory
{
    public IFigure CreateFigure(int widthPixels)
    {
        var finalXStartPosition = (widthPixels / 2);
        var square = new Square(finalXStartPosition);
        var line = new Line(finalXStartPosition);
        var z = new Z(finalXStartPosition);

        var figures = new List<IFigure> { square, line, z };
        
        var random = new Random();
        return figures[random.Next(figures.Count)];
    }
}