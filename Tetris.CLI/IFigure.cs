public interface IFigure
{
    Orientation Direction { get; set; }
    Position[] Position { get; set; }
    bool IsMoving { get; set; }
    Position[] Rotate();

    public enum Orientation
    {
        Vertical, Horizontal
    }
}
